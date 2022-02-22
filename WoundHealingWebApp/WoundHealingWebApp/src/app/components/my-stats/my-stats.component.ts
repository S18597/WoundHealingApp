import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';
import { StatsDto } from 'src/app/DTOs/StatsDto';
import { WoundTypesStats } from 'src/app/DTOs/WoundTypesStats';
import { BarModel } from 'src/app/DTOs/BarModel';
import { Location } from '@angular/common';

@Component({
  selector: 'app-my-stats',
  templateUrl: './my-stats.component.html',
  styleUrls: ['./my-stats.component.css', './bar-styles.scss']
})
export class MyStatsComponent implements OnInit {

  myStats!: StatsDto;
  woundTypesStats!: WoundTypesStats[];

  List: BarModel[] = [];

  public Total=0;
  public MaxHeight= 160;

  myPatients = 0;
  finishedTreatments = 0;
  myAppointments = 0;
  avgTreatmentTime = 0;
  wounds = 0;

  doctorId!: number | undefined;

  constructor(private api: ApiService, private router: Router, private _location: Location) {
    this.doctorId = this.api.loggedUserId;
    console.log('logged doctor id: ', this.doctorId);
  }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }  

  MountBars(){
    if(this.List && this.List.length > 0){
      console.log('mount bars');
      this.List.forEach(element => {
        this.Total += element.value;
      });

      this.List.forEach(element => {
        element.size = Math.round((element.value*this.MaxHeight)/this.Total) + '%';
      });
    }
  }

  loadBarList() {
    console.log('load bar list');
    if(this.woundTypesStats && this.woundTypesStats.length > 0){
      this.woundTypesStats.forEach(w => {
        let bar = new BarModel();
        bar.color = '#c4d9fc';
        bar.value = w.woundTypeCnt;
        bar.legend = w.woundType;
        bar.size = '';
        this.List.push(bar);
      });
    }
  }

  async loadData() {
    console.log('load data');
    await this.loadMyStats();
    this.loadBarList();
    this.MountBars();
  }

  async loadMyStats() {
    console.log('load my stats doctorId: ', this.doctorId);
    this.myStats = await this.api.getDoctorStats(this.doctorId);
    console.log('loaded my stats: ', this.myStats);

    this.myPatients = this.myStats.patientsCnt;
    this.finishedTreatments = this.myStats.finishedTreatmentsCnt;
    this.myAppointments = this.myStats.appointmentsCnt;
    this.avgTreatmentTime = this.myStats.avgTreatmentDays;
    this.wounds = this.myStats.woundsCnt;

    this.woundTypesStats = this.myStats.woundTypesStats;
    console.log('loaded wound types stats: ', this.woundTypesStats);
  }

  ngOnInit(): void {
    (async () => {
      await this.loadData();
    })();
  }
}