import { Component, OnInit } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';

declare var require: any;
const md5 = require('md5');

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private apiService: ApiService, private router: Router, private route: ActivatedRoute) { }

  email: string | undefined;
  password: string | undefined;
  return = '';

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => this.return = params.return);
  }

  async login(): Promise<void> {

    if (!this.email) {
      alert('Nie wprowadzono nazwy użytkownika!');
      return;
    }
    if (!this.password) {
      alert('Nie wprowadzono hasła!');
      return;
    }

    const user = await this.apiService.getUserByEmail(this.email);
    console.log('user: ', user);

    if(!user) {
      console.log('user not found!');
      return;
    }

    const auth = await this.apiService.getUserAuthByUserId(user.userId);
    console.log('auth: ', auth);

    if(!auth) {
      console.log('user auth not found!');
      return;
    }

    const hash = md5(auth.salt, this.password);
    if(hash === auth.hash) {
      this.apiService.isLogged = true;
      this.apiService.loggedUserId = auth.userId;
      this.apiService.loggedUserEmail = user.emailAddress;
      this.apiService.loggedUserIsDoctor = user.isDoctor;
      this.apiService.loggedUserIsPatient = user.isPatient;
    }
    else {
      alert('Unsuccessfull login!');
      return;
    }

    // redirect to home
    this.router.navigateByUrl('/main');
  }
}