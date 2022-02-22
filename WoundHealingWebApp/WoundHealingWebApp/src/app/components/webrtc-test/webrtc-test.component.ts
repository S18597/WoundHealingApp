import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
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
  selector: 'app-webrtc-test',
  templateUrl: './webrtc-test.component.html',
  styleUrls: ['./webrtc-test.component.css']
})
export class WebrtcTestComponent implements OnInit {
  @ViewChild('webcamVideo') webcamVideo!: ElementRef<HTMLVideoElement>
  @ViewChild('remoteVideo') remoteVideo!: ElementRef<HTMLVideoElement>

  localStream!: MediaStream;
  remoteStream!: MediaStream;

  callButtonDisabled = true;
  answerButtonDisabled = true;
  webcamButtonDisabled = false;
  hangupButtonDisabled = true;

  callInput!: string;

  constructor(private firestore: AngularFirestore) { }

  // SETUP MEDIA SOURCES //
  async webcamButton() {
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

    this.callButtonDisabled = false;
    this.answerButtonDisabled = false;
    this.webcamButtonDisabled = true;
  }

  // CREATE AN OFFER //
  async callButton(){
    const db = this.firestore.firestore;

    const callDoc = db.collection('calls').doc();
    const offerCandidates = callDoc.collection('offerCandidates');
    const answerCandidates = callDoc.collection('answerCandidates');

    this.callInput = callDoc.id;

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

    this.hangupButtonDisabled = false;
  }

  // 3. ANSWER the call with the unique ID
  async answerButton() {
    const db = this.firestore.firestore;

    const callId = this.callInput;
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
  
  async hangupButton() {
  }

  ngOnInit(): void {
      
  }
}