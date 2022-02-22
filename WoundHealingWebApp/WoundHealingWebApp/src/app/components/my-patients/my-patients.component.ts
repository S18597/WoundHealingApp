import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MyPatientDto } from 'src/app/DTOs/MyPatientDto';
import { ApiService } from 'src/app/services/api.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-my-patients',
  templateUrl: './my-patients.component.html',
  styleUrls: ['./my-patients.component.css']
})
export class MyPatientsComponent implements OnInit {

  showMyPatients = false;
  myPatients: MyPatientDto[] = [];

  isDoctor = false;

  displayedColumns: string[] = [
    'patientName', 
    'patientEmail', 
    'view'
  ];
  dataSource = new MatTableDataSource(this.myPatients);

  constructor(private api: ApiService, private router: Router, private _location: Location) { }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }

  async onPatientData(element: any) {
    console.log('view patient data: ', element);
    console.log('selected patientId: ', element.patientId);
    this.api.doctorsSelectedPatientId = element.patientId;
    this.api.doctorsSelectedPatientEmail = element.patientEmail;
    this.router.navigateByUrl('/patient-data');
  }

  async loadData() {
    console.log('load data');
    await this.loadMyAppointments();
  }

  async loadMyAppointments(){
    console.log('load my patients doctorId: ', this.api.loggedUserId);
    this.myPatients = await this.api.getMyPatients(this.api.loggedUserId);
    console.log('loaded my patients: ', this.myPatients);
    this.dataSource = new MatTableDataSource(this.myPatients);
    if(this.myPatients && this.myPatients.length > 0){
      this.showMyPatients = true;
    }
    else{
      this.showMyPatients = false;
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {
    this.isDoctor = this.api.loggedUserIsDoctor!;
    console.log('isDoctor: ', this.isDoctor);
    (async () => {
      await this.loadData();
    })();
  }
}