import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router, private api: ApiService) { }

  async login(event: any){
    console.log('go to login');
    this.router.navigateByUrl('/login');
  }

  async createAccount(event: any){
    console.log('go to createAccount');
    this.router.navigateByUrl('/create');
  }

  ngOnInit(): void {
  }

}