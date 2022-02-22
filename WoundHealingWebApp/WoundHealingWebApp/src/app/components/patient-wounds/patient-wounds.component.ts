import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MyPatientWoundDto } from 'src/app/DTOs/MyPatientWoundDto';
import { ApiService } from 'src/app/services/api.service';
import { Location } from '@angular/common';
import { FinishTreatmentDto } from 'src/app/DTOs/FinishTreatmentDto';

@Component({
  selector: 'app-patient-wounds',
  templateUrl: './patient-wounds.component.html',
  styleUrls: ['./patient-wounds.component.css']
})
export class PatientWoundsComponent implements OnInit {

  showMyPatientsWounds = false;
  myPatientsWounds: MyPatientWoundDto[] = [];

  isDoctor = false;

  displayedColumns: string[] = [
    'patientName', 
    'patientEmail', 
    'patientWoundType',
    'patientWoundPhoto',
    'details',
    'treatment'
  ];
  dataSource = new MatTableDataSource(this.myPatientsWounds);

  constructor(private api: ApiService, private router: Router, private _location: Location) { }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }

  async onPatientWound(element: any){
    console.log('view patient wound data: ', element);
    console.log('selected patientId: ', element.patientId);
    console.log('selected woundId: ', element.woundId);
    this.router.navigateByUrl('/wound-details/' + element.woundId);
  }

  async onTreatmentFinished(element: any) {
    console.log('treatment finished wound data: ', element);
    console.log('selected patientId: ', element.patientId);
    console.log('selected woundId: ', element.woundId);
    let finishTreatmentDto: FinishTreatmentDto = {
      doctorId: this.api.loggedUserId,
      patientId: element.patientId,
      woundId: element.woundId
    }
    console.log('finished treatment dto: ', finishTreatmentDto);
    await this.api.finishTreatment(finishTreatmentDto);
    alert('Treatment finished');
  }

  async loadMyPatientsWounds(){
    console.log('load my patients wounds doctorId: ', this.api.loggedUserId);
    this.myPatientsWounds = await this.api.getMyPatientsWounds(this.api.loggedUserId);

    this.myPatientsWounds.forEach(w => {
      let photoId = w.woundPhotoId;
      if(photoId > 0){
        const filename = this.api.getPhoto2(photoId);
        console.log('laoded wound photo: ', filename);
        let imgPath = `/assets/images/${photoId}.jpeg`;
        w.woundPhoto = imgPath;
      }
    });
    
    console.log('loaded my patients wounds: ', this.myPatientsWounds);
    this.dataSource = new MatTableDataSource(this.myPatientsWounds);
    if(this.myPatientsWounds && this.myPatientsWounds.length > 0){
      this.showMyPatientsWounds = true;
    }
    else{
      this.showMyPatientsWounds = false;
    }
  }

  async loadData() {
    console.log('load data');
    await this.loadMyPatientsWounds();
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