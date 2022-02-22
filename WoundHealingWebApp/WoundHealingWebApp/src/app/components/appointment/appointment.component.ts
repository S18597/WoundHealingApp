import { Component, OnInit, Inject } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentSummaryDto } from 'src/app/DTOs/AppointmentSummaryDto';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AppointmentNotesComponent } from '../appointment-notes/appointment-notes.component';
import { Location } from '@angular/common';

export interface DialogData {
  appointmentId: string | null;
  appointmentNote: string;
  isPatient: boolean;
}

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  appointmentId!: string | null;
  woundId!: number;

  userId!: number | undefined;
  userEmail!: string;
  isPatient!: boolean | undefined;
  isDoctor!: boolean | undefined;

  appointmentSummary!: AppointmentSummaryDto;
  appointmentNote!: string | null;

  constructor(private route: ActivatedRoute, private api: ApiService, private router: Router, public dialog: MatDialog, private _location: Location) {
    this.userId = this.api.loggedUserId;
    this.userEmail = this.api.loggedUserEmail;
    this.isPatient = this.api.loggedUserIsPatient;
    this.isDoctor = this.api.loggedUserIsDoctor;
  }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }

  async loadData() {
    console.log('load data');
    await this.loadPatientAppointmentSummary();
  }

  async loadPatientAppointmentSummary() {
    console.log('load appointment summary for id: ', this.appointmentId);
    this.appointmentSummary = await this.api.getAppointmentSummery(this.appointmentId);
    this.woundId = this.appointmentSummary.woundId;
    console.log('loaded appointment summary: ', this.appointmentSummary);
  }

  async openDialog() {
    this.appointmentNote = await (await this.api.getAppointmentNote(this.appointmentId)).appointmentNote;
    console.log('loaded note: ', this.appointmentNote);
    const dialogRef = this.dialog.open(AppointmentNotesComponent, {
      width: '800px',
      // height: '300px',
      data: {appointmentId: this.appointmentId, appointmentNote: this.appointmentNote, isPatient: this.isPatient}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog is closed');
    });
  }

  // navigation
  async onWoundDetails(){
    console.log('on wound details');
    this.router.navigateByUrl('/wound-details/' + this.woundId);
  }

  async onAppointmentNotes(){
    console.log('on appointment notes');
    //alert('on appointment notes');
    await this.openDialog();
  }

  async onChat(){
    console.log('on chat');
    this.router.navigateByUrl('/chat');
  }

  async onMedicalData(){
    console.log('on medical data');
    this.router.navigateByUrl('/patient-data');
  }

  async onSetUpAppointment(){
    console.log('on set up appointment');
    this.router.navigateByUrl('/register-appointment');
  }

  async onVideoCall() {
    console.log('on video call');
    this.router.navigateByUrl('/video-call');
  }


  ngOnInit(): void {
    this.route.paramMap.subscribe( params => {
      let id = params.get('appointmentId');  
      console.log('got appointment id: ', id);
      this.appointmentId = id;
    });

    (async () => {
      await this.loadData();
    })();
  }
}