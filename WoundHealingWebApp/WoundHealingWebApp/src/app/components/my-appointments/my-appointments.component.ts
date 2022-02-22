import { Component, OnInit } from '@angular/core';
import { MyAppointmentDto } from 'src/app/DTOs/MyAppointmentDto';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { MatTableDataSource } from '@angular/material/table';
import { MyAppointmentDocDto } from 'src/app/DTOs/MyAppointmentDocDto';
import { Location } from '@angular/common';

@Component({
  selector: 'app-my-appointments',
  templateUrl: './my-appointments.component.html',
  styleUrls: ['./my-appointments.component.css']
})
export class MyAppointmentsComponent implements OnInit {

  isPatient = true;

  showMyAppointments = false;
  myAppointments: MyAppointmentDto[] = [];

  displayedColumns: string[] = [
    'appointmentDate', 
    'doctorName', 
    'appointmentNotes',
    'view',
    'cancel'
  ];
  dataSource = new MatTableDataSource(this.myAppointments);

  myAppointmentsDoc: MyAppointmentDocDto[] = [];

  displayedColumnsDoc: string[] = [
    'appointmentDate', 
    'patientName', 
    'appointmentNotes',
    'view',
    'cancel'
  ];
  dataSourceDoc = new MatTableDataSource(this.myAppointmentsDoc);

  constructor(private apiService: ApiService, private router: Router, private _location: Location) { }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }
  
  async loadData() {
    if(this.isPatient) {
      await this.loadMyAppointments();
    }
    else {
      await this.loadMyAppointmentsDoc();
    }
  }

  async loadMyAppointments(){
    console.log('load my appointments userId: ', this.apiService.loggedUserId);
    this.myAppointments = await this.apiService.getMyAppointments(this.apiService.loggedUserId);
    console.log('loaded my appointments: ', this.myAppointments);
    this.dataSource = new MatTableDataSource(this.myAppointments);
    if(this.myAppointments && this.myAppointments.length > 0){
      this.showMyAppointments = true;
    }
    else{
      this.showMyAppointments = false;
    }
  }

  async loadMyAppointmentsDoc(){
    console.log('load my appointments doc userId: ', this.apiService.loggedUserId);
    this.myAppointmentsDoc = await this.apiService.getMyAppointmentsDoc(this.apiService.loggedUserId);
    console.log('loaded my appointments doc: ', this.myAppointmentsDoc);
    this.dataSourceDoc = new MatTableDataSource(this.myAppointmentsDoc);
    if(this.myAppointmentsDoc && this.myAppointmentsDoc.length > 0){
      this.showMyAppointments = true;
    }
    else{
      this.showMyAppointments = false;
    }
  }

  async onViewAppointment(element: any) {
    console.log('view appointment: ', element);
    this.router.navigateByUrl('/appointment/' + element?.appointmentId );
  }

  async onViewAppointmentDoc(element: any) {
    console.log('view appointment doc: ', element);
  }

  async onDeleteAppointment(element: any){
    console.log('delete appointment: ', element);
    await this.apiService.deleteAppointment(element?.appointmentId);
    await this.loadMyAppointments();
  }

  async onDeleteAppointmentDoc(element: any){
    console.log('delete appointment doc: ', element);
    await this.apiService.deleteAppointment(element?.appointmentId);
    await this.loadMyAppointmentsDoc();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  applyFilterDoc(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceDoc.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {

    this.isPatient = this.apiService.loggedUserIsPatient!;
    console.log('isPatient: ', this.isPatient);

    (async () => {
      await this.loadData();
    })();
  }
}