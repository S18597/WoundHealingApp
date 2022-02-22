import { Component, OnInit } from '@angular/core';
import { MyWoundDto } from 'src/app/DTOs/MyWoundDto';
import { ApiService } from 'src/app/services/api.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { FastestAppointmentDto } from 'src/app/DTOs/FastestAppointmentDto';
import { FindAppointmentDto } from 'src/app/DTOs/FindAppointmentDto';
import { DoctorDto } from 'src/app/DTOs/DoctorDto';
import { CreateAppointmentDto } from 'src/app/DTOs/CreateAppointmentDto';
import { Location } from '@angular/common';

@Component({
  selector: 'app-register-appointment',
  templateUrl: './register-appointment.component.html',
  styleUrls: ['./register-appointment.component.css']
})
export class RegisterAppointmentComponent implements OnInit {

  myWounds: MyWoundDto[] = [];
  showMyWounds = true;
  showAppointment = false;
  showDoctors = false;

  displayedColumns: string[] = [
    'woundRegisterDate', 
    'woundType', 
    'woundLocation', 
    'woundSize', 
    'woundColor', 
    'woundOdor', 
    'woundExudate', 
    'woundBleeding', 
    'surroundingSkin',
    'painType',
    'painLevel',
    'appointment'
  ];
  dataSource = new MatTableDataSource(this.myWounds);

  selectedDate: Date | undefined = new Date();
  selectedDoctorId: number | undefined;
  fastestAppointment: FastestAppointmentDto | undefined = undefined;

  selectedWoundId = 0;

  appointments: FastestAppointmentDto[] = [];
  displayedColumnsApp: string[] = [
    'doctorFullname',
    'appointmentDate',
    'appointment'
  ];
  dataSourceApp = new MatTableDataSource(this.appointments);

  doctors: DoctorDto[] = [];

  constructor(private apiService: ApiService, private router: Router, private _location: Location) { }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }  

  async loadMyWounds(){
    console.log('load my wounds userId: ', this.apiService.loggedUserId);
    this.myWounds = await this.apiService.getMyWounds(this.apiService.loggedUserId);
    console.log('loaded my wounds: ', this.myWounds);
    this.dataSource = new MatTableDataSource(this.myWounds);
    if(this.myWounds && this.myWounds.length > 0){
      this.showMyWounds = true;
    }
    else{
      this.showMyWounds = false;
    }
  }

  async loadAppointments() {
    console.log('load appointments');

    let date = this.selectedDate;
    date!.setHours(date!.getHours() + 1);
    let findAppointmentDto: FindAppointmentDto = {
      date: date,
      doctorId: this.selectedDoctorId
    };

    this.appointments = await this.apiService.getAppointmentsPerDate(findAppointmentDto);
    this.dataSourceApp = new MatTableDataSource(this.appointments);
    console.log('loaded appointments: ', this.appointments);
  }

  async loadDoctors(){
    console.log('load doctors');
    this.doctors = await this.apiService.getDoctors();
    console.log('loaded doctors: ', this.doctors);
  }

  async onCreateAppointment(element: any){
    console.log('appointment: ', element);

    this.selectedWoundId = element.woundId;
    console.log('selected woundId: ', this.selectedWoundId);

    // get fastest available visit
    this.fastestAppointment = await this.apiService.getFastestAppointment();
    console.log('fastest appointment: ', this.fastestAppointment);

    await this.loadDoctors();
    await this.loadAppointments();

    this.showMyWounds = false;
    this.showAppointment = true;
  }

  async onDateChange(){
    await this.loadAppointments();
  }

  async onAddAppointment(visit: any) {
    console.log('selected visit: ', visit);

    let createAppointmentDto: CreateAppointmentDto = {
      doctorId: visit.doctorId,
      appointmentDate: visit.appointmentDate,
      patientId: this.apiService.loggedUserId,
      woundId: this.selectedWoundId
    };
    
    console.log('create appointment: ', createAppointmentDto);
    await this.apiService.createAppointment(createAppointmentDto);

    alert('Appointment created');
    // redirect
    this.router.navigateByUrl('/main');
  }

  async onApproveFastestAppointment() {
    console.log('onApproveFastestAppointment');

    let createAppointmentDto: CreateAppointmentDto = {
      woundId: this.selectedWoundId,
      doctorId: this.fastestAppointment?.doctorId,
      patientId: this.apiService.loggedUserId,
      appointmentDate: this.fastestAppointment?.appointmentDate
    };

    console.log('create fastest appointment: ', createAppointmentDto);
    await this.apiService.createAppointment(createAppointmentDto);
    
    alert('Appointment created');
    // redirect
    this.router.navigateByUrl('/main');
  }

  async onAddWound(){
    console.log('onAddWound');
    this.router.navigateByUrl('/register-wound');
  }

  chooseDoctor() {
    console.log('choose a doctor');
    this.showDoctors = true;
  }

  async onDoctorChange() {
    console.log('doctor changed to id: ', this.selectedDoctorId);
    await this.loadAppointments();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  applyFilterApp(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceApp.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {
      (async () => {
        await this.loadMyWounds();
      })();
  }

}