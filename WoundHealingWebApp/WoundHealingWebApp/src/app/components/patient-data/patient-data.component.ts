import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { MedicalDataDto } from 'src/app/DTOs/MedicalDataDto';
import { MyMedicalDataDto } from 'src/app/DTOs/MyMedicalDataDto';
import { Location } from '@angular/common';

@Component({
  selector: 'app-patient-data',
  templateUrl: './patient-data.component.html',
  styleUrls: ['./patient-data.component.css']
})
export class PatientDataComponent implements OnInit {

  medicalData!: MyMedicalDataDto;
  userId!: number | undefined;
  userEmail!: string;
  isPatient!: boolean | undefined;

  isEditMode = false;

  patientData!: User;

  chronicDiseases = '';
  medication = '';
  allergies = '';

  isPregnancy = false;
  isTobacco = false;
  isAlcohol = false;
  isDrugs = false;

  constructor(private router: Router, private api: ApiService, private _location: Location) { 
    this.userId = this.api.loggedUserId;
    this.userEmail = this.api.loggedUserEmail;
    this.isPatient = this.api.loggedUserIsPatient;
    if(this.isPatient){
      this.isEditMode = true;
    }
    if(this.api.doctorsSelectedPatientId){
      this.userId = this.api.doctorsSelectedPatientId;
      this.userEmail = this.api.doctorsSelectedPatientEmail;
    }
  }

  async goToMain() {
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }

  async loadData() {
    await this.loadPatientData();
    await this.loadMyMedicalData();
  }

  async loadMyMedicalData() {
    console.log('load medical data');
    this.medicalData = await this.api.getMyMedicalData(this.userId);

    this.chronicDiseases = this.medicalData.chronicDeseases;
    this.medication = this.medicalData.medication;
    this.allergies = this.medicalData.allergies;
    this.isAlcohol = this.medicalData.alcohol;
    this.isDrugs = this.medicalData.drugs;
    this.isPregnancy = this.medicalData.pregnancy;
    this.isTobacco = this.medicalData.tobacco;

    console.log('loaded medical data: ', this.medicalData);
  }

  async loadPatientData() {
    console.log('load patient data');
    this.patientData = await this.api.getUserByEmail(this.userEmail);
    console.log('loaded patient data: ', this.patientData);
  }

  async saveMedicalData() {
    console.log('save medical data');
    let data: MedicalDataDto = {
      userId: this.userId,
      chronicDiseases: this.chronicDiseases,
      allergies: this.allergies,
      medication: this.medication,
      alcohol: this.isAlcohol,
      drugs: this.isDrugs,
      pregnancy: this.isPregnancy,
      tobacco: this.isTobacco
    };
    await this.api.saveMedicalData(data);
    console.log('saved medical data: ', data);
    alert('Medical Data Saved');
  }

  ngOnInit(): void {
    (async () => {
      await this.loadData();
    })();
  }
}