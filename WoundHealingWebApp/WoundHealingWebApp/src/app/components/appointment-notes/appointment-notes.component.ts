import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { AppointmentNoteDto } from 'src/app/DTOs/AppointmentNoteDto';

export interface DialogData {
  appointmentId: string;
  appointmentNote: string;
  isPatient: boolean;
}

@Component({
  selector: 'app-appointment-notes',
  templateUrl: './appointment-notes.component.html',
  styleUrls: ['./appointment-notes.component.css']
})
export class AppointmentNotesComponent {
  note!: string;
  id!: number;
  isPatient!: boolean;

  constructor(public dialogRef: MatDialogRef<AppointmentNotesComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private api: ApiService) {
    this.id = parseInt(data.appointmentId);
    this.note = data.appointmentNote;
    this.isPatient = data.isPatient;

    console.log('loaded note: ', this.note);
  }

  onCancel() {
    this.dialogRef.close();
  }

  async onSave() {
    console.log(`on save - appointmentId: ${this.id}, note: ${this.note}`);
    let appointmentNoteDto: AppointmentNoteDto = {
      appointmentId: this.id,
      appointmentNote: this.note
    };
    await this.api.saveAppointmentNote(appointmentNoteDto);
    this.dialogRef.close();
  }
}