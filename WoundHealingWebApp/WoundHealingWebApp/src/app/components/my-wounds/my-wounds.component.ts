import { Component, OnInit } from '@angular/core';
import { MyWoundDto } from 'src/app/DTOs/MyWoundDto';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { MatTableDataSource } from '@angular/material/table';
import { Location } from '@angular/common';


@Component({
  selector: 'app-my-wounds',
  templateUrl: './my-wounds.component.html',
  styleUrls: ['./my-wounds.component.css']
})
export class MyWoundsComponent implements OnInit {

  myWounds: MyWoundDto[] = [];
  showMyWounds = true;

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
    'view',
    'delete'
  ];
  dataSource = new MatTableDataSource(this.myWounds);

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

  async onDeleteWound(element: any) {
    console.log('delete wound: ', element);
    await this.apiService.deleteWound(element.woundId);
    console.log('deleted wound');

    await this.loadMyWounds();
  }

  // navigation
  async onWoundDetails(element: any) {
    console.log('on wound details: ', element);
    this.router.navigateByUrl('/wound-details/' + element.woundId);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {
    (async () => {
      await this.loadMyWounds();
    })();
  }
}