import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { Router } from '@angular/router';
import 'firebase/firestore';
import { Location } from '@angular/common';

const servers = {
  iceServers: [
    {
      urls: ['stun:stun1.l.google.com:19302', 'stun:stun2.l.google.com:19302'],
    },
  ],
  iceCandidatePoolSize: 10,
};
const pc = new RTCPeerConnection(servers);
const link = "https://5607-2a02-a310-e041-1380-7915-3ceb-77c7-fbcb.ngrok.io/?call=";

@Component({
  selector: 'app-video-call',
  templateUrl: './video-call.component.html',
  styleUrls: ['./video-call.component.css']
})
export class VideoCallComponent implements OnInit {
  @ViewChild('webcamVideo') webcamVideo!: ElementRef<HTMLVideoElement>
  @ViewChild('remoteVideo') remoteVideo!: ElementRef<HTMLVideoElement>

  localStream!: MediaStream;
  remoteStream!: MediaStream;

  callInput!: string;

  constructor(private firestore: AngularFirestore, private router: Router, private _location: Location) { }

  async goToMain(){
    this.localStream.getTracks().forEach((track) => {
      track.stop();
    });
    this.remoteStream.getTracks().forEach((track) => {
      track.stop();
    });
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this.localStream.getTracks().forEach((track) => {
      track.stop();
    });
    this.remoteStream.getTracks().forEach((track) => {
      track.stop();
    });
    this._location.back();
  }

  async webcamSetup() {
    this.localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
    this.remoteStream = new MediaStream();

    // Push tracks from local stream to peer connection
    this.localStream.getTracks().forEach((track) => {
      console.log('local stream add track: ', track);
      pc.addTrack(track, this.localStream);
    });

    // Pull tracks from remote stream, add to video stream
    pc.ontrack = (event) => {
      event.streams[0].getTracks().forEach((track) => {
        console.log('remote stream add track: ', track);
        this.remoteStream.addTrack(track);
      });
    };

    // Show stream in HTML video
    this.webcamVideo.nativeElement.srcObject = this.localStream;
    this.remoteVideo.nativeElement.srcObject = this.remoteStream;
  }

  async sendChatLink() {
    console.log('send chat link');
  }

  async makeCall() {
    const db = this.firestore.firestore;

    const callDoc = db.collection('calls').doc();
    const offerCandidates = callDoc.collection('offerCandidates');
    const answerCandidates = callDoc.collection('answerCandidates');

    this.callInput = link + callDoc.id;

    // Get candidates for caller, save to db
    pc.onicecandidate = (event) => {
      console.log('offer onicecandidate event: ', event);
      event.candidate && offerCandidates.add(event.candidate.toJSON());
      console.log('offer candidate: ', event.candidate?.toJSON());
    };

     // Create offer
    const offerDescription = await pc.createOffer();
    await pc.setLocalDescription(offerDescription);

    const offer = {
      sdp: offerDescription.sdp,
      type: offerDescription.type,
    };
    console.log('created an offer: ', offer);

    await callDoc.set({offer});
    console.log('db offer set');

    //Listen for remote answer
    callDoc.onSnapshot((snapshot) => {
      const data = snapshot.data();
      if (!pc.currentRemoteDescription && data?.answer) {
        const answerDescription = new RTCSessionDescription(data.answer);
        pc.setRemoteDescription(answerDescription);
        console.log('set remote description: ', answerDescription);
      }
    });

    // When answered, add candidate to peer connection
    answerCandidates.onSnapshot((snapshot) => {
      snapshot.docChanges().forEach((change) => {
        if (change.type === 'added') {
          const candidate = new RTCIceCandidate(change.doc.data());
          pc.addIceCandidate(candidate);
          console.log('offer added ice candidate: ', candidate);
        }
      });
    });

    // send chat link to patient
    await this.sendChatLink();
  }


  ngOnInit(): void {
    (async () => {
      await this.webcamSetup();
    })();
  }
}