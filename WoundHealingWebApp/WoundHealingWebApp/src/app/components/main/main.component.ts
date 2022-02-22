import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MyAppointmentDocDto } from 'src/app/DTOs/MyAppointmentDocDto';
import { WoundAppointmentDto } from 'src/app/DTOs/WoundAppointmentDto';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  patientWoundAppointments: WoundAppointmentDto[] = [];
  doctorAppointments: MyAppointmentDocDto[] = [];

  isDoctor: boolean | undefined = false;
  isPatient: boolean | undefined = false;

  constructor(private router: Router, private api: ApiService) { 
    this.isDoctor = api.loggedUserIsDoctor;
    this.isPatient = api.loggedUserIsPatient;

    console.log('isPatient: ', this.isPatient);
    console.log('isDoctor: ', this.isDoctor);
  }

  
  // data

  async loadData() {
    console.log('load data');
    if(this.isPatient) {
      await this.loadPatientData();
    }
    if(this.isDoctor){
      await this.loadDoctorData();
    }
  }

  async loadPatientData() {
    console.log('load patient data');
    await this.loadMyWoundsAppointments();
  }

  async loadDoctorData() {
    console.log('load doctor data');
    await this.loadDoctorAppointments();
  }

  async loadMyWoundsAppointments() {
    console.log('load patient wounds appointments');
    this.patientWoundAppointments = await this.api.getMyWoundAppointments(this.api.loggedUserId);
    console.log('loaded patient wound appointments: ', this.patientWoundAppointments);
  }

  async loadDoctorAppointments() {
    console.log('load doctor appointments');
    this.doctorAppointments = await this.api.getMyAppointmentsDoc(this.api.loggedUserId); //VK
    console.log('loaded doctor appointments: ', this.doctorAppointments);
  }

  // navigation
  async myData() {
    console.log('my data');
    this.router.navigateByUrl('/patient-data');
  }

  async myPatients() {
    console.log('my patients');
    this.router.navigateByUrl('/my-patients');
  }

  async registerWound(event: any) {
    console.log('register wound');
    this.router.navigateByUrl('/register-wound');
  }

  async myWounds(event: any) {
    console.log('my wounds');
    this.router.navigateByUrl('/my-wounds');
  }

  async wounds(){
    console.log('patient wounds');
    this.router.navigateByUrl('/patient-wounds');
  }

  async myStats(){
    console.log('my stats');
    this.router.navigateByUrl('/my-stats');
  }

  async registerAppointment(event: any) {
    console.log('register appointment');
    this.router.navigateByUrl('/register-appointment');
  }

  async myAppointments(event: any) {
    console.log('my appointments');
    this.router.navigateByUrl('/my-appointments');
  }

  async chat(event: any) {
    console.log('chat');
    this.router.navigateByUrl('/chat');
  }

  async webRTCtest(event: any) {
    console.log('webRTC test');
    this.router.navigateByUrl('/webrtc-test');
  }

  ngOnInit(): void {
    (async () => {
      await this.loadData();
    })();
  }
}