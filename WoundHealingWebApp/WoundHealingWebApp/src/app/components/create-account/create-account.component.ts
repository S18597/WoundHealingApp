import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { createUserDto } from '../../DTOs/createUserDto';
import { ApiService } from 'src/app/services/api.service';

const md5 = require('md5');
declare var require: any

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  registerForm = new FormGroup({
    accountType: new FormControl('Patient'),
    firstname: new FormControl(null, Validators.required),
    lastname: new FormControl(null, Validators.required),
    email: new FormControl(null, Validators.required),
    password1: new FormControl(null, Validators.required),
    password2: new FormControl(null, Validators.required),
    dateOfBirth: new FormControl(null, Validators.required),
    pesel: new FormControl(null, Validators.required),
    phoneNumber: new FormControl(null, Validators.required),
    address: new FormControl(null, Validators.required),
  });

  accountTypes: string[] = ['Patient', 'Doctor'];
  selectedAccountType: string = 'Patient';

  apiService: ApiService;

  constructor(apiService: ApiService, private router: Router, private route: ActivatedRoute) { 
    this.apiService = apiService;
  }

  get accountType(): any {
    return this.registerForm.get('accountType');
  }

  get firstname(): any {
    return this.registerForm.get('firstname');
  }

  get lastname(): any {
    return this.registerForm.get('lastname');
  }

  get email(): any {
    return this.registerForm.get('email');
  }

  get password1(): any {
    return this.registerForm.get('password1');
  }

  get password2(): any {
    return this.registerForm.get('password2');
  }

  get dateOfBirth(): any {
    return this.registerForm.get('dateOfBirth');
  }

  get pesel(): any {
    return this.registerForm.get('pesel');
  }

  get phoneNumber(): any {
    return this.registerForm.get('phoneNumber');
  }

  get address(): any {
    return this.registerForm.get('address');
  }

  async onRegister() {
    console.log('onRegister', this.registerForm.value);

    // password
    let pass1: string = this.password1.value.toString();
    let pass2: string = this.password2.value.toString();
    if (pass1 !== pass2) {
      console.log('passwords are not the same!');
      alert('passwords are not the same!');
      return;
    }

    // password hash
    let salt = this.salt(4);
    let pass = pass1;
    let hash = md5(salt, pass);

    // dto
    let createUserDto: createUserDto = {
      accountType: this.accountType.value.toString() === 'Patient' ? 1 : 2,
      firstname: this.firstname.value,
      lastname: this.lastname.value,
      pesel: this.pesel.value,
      address: this.address.value,
      phoneNumber: this.phoneNumber.value,
      emailAddress: this.email.value,
      dateOfBirth: this.dateOfBirth.value,
      salt: salt,
      hash: hash
    };

    // api
    await this.apiService.createUser(createUserDto);
    console.log("User created");

    // redirect to login
    this.router.navigateByUrl('/login');
  }

  salt(length: number) {
    let text = '';
    const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (let i = 0; i < length; i++) {
      text += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return text;
  }

  ngOnInit(): void {
  }

}