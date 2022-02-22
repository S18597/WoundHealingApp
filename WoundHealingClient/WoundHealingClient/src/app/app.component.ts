import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { ActivatedRoute } from '@angular/router';
import 'firebase/firestore';

const servers = {
  iceServers: [
    {
      urls: ['stun:stun1.l.google.com:19302', 'stun:stun2.l.google.com:19302'],
    },
  ],
  iceCandidatePoolSize: 10,
};
const pc = new RTCPeerConnection(servers);

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  @ViewChild('webcamVideo') webcamVideo!: ElementRef<HTMLVideoElement>
  @ViewChild('remoteVideo') remoteVideo!: ElementRef<HTMLVideoElement>

  localStream!: MediaStream;
  remoteStream!: MediaStream;

  callId!: string;

  constructor(private firestore: AngularFirestore, private route: ActivatedRoute) { }

  mediaSetup() {

  }

  async setupStream() {
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
        // todo check for audio tracks and disable one
        this.remoteStream.addTrack(track);
      });
    };

    // Show stream in HTML video
    this.webcamVideo.nativeElement.srcObject = this.localStream;
    this.remoteVideo.nativeElement.srcObject = this.remoteStream;

    // this.callButtonDisabled = false;
    // this.answerButtonDisabled = false;
    // this.webcamButtonDisabled = true;
  }

  async answerCall() {
    const db = this.firestore.firestore;

    const callId = this.callId;
    const callDoc = db.collection('calls').doc(callId);
    const answerCandidates = callDoc.collection('answerCandidates');
    const offerCandidates = callDoc.collection('offerCandidates');

    pc.onicecandidate = (event) => {
      console.log('answer onicecandidate event: ', event);
      event.candidate && answerCandidates.add(event.candidate.toJSON());
      console.log('answer candidate: ', event.candidate?.toJSON());
    };

    console.log('get call data')
    const callData = (await callDoc.get()).data();
    console.log('call data: ', callData);

    const offerDescription = callData!.offer;
    console.log('answer - get an offer: ', offerDescription);
    await pc.setRemoteDescription(new RTCSessionDescription(offerDescription));
    console.log('remote description set');

    console.log('create an answer');
    const answerDescription = await pc.createAnswer();
    await pc.setLocalDescription(answerDescription);
    console.log('local description set');

    const answer = {
      type: answerDescription.type,
      sdp: answerDescription.sdp,
    };
    console.log('created an answer: ', answer);

    console.log('update call in db with answer data');
    await callDoc.update({ answer });

    offerCandidates.onSnapshot((snapshot) => {
      snapshot.docChanges().forEach((change) => {
        console.log(change);
        if (change.type === 'added') {
          let data = change.doc.data();
          pc.addIceCandidate(new RTCIceCandidate(data));
          console.log('answer added ice candidate: ', data);
        }
      });
    });
  }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
        console.log(params);
        this.callId = params.call;
        console.log('got call id: ', this.callId);
      }
    );
    
    (async () => {
      await this.setupStream();
      await this.answerCall();
    })();
  }
}