import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { PainLevel } from 'src/app/models/PainLevel';
import { PainType } from 'src/app/models/PainType';
import { SurroundingSkin } from 'src/app/models/SurroundingSkin';
import { WoundBleeding } from 'src/app/models/WoundBleeding';
import { WoundColor } from 'src/app/models/WoundColor';
import { WoundExudate } from 'src/app/models/WoundExudate';
import { WoundLocation } from 'src/app/models/WoundLocation';
import { WoundOdor } from 'src/app/models/WoundOdor';
import { WoundSize } from 'src/app/models/WoundSize';
import { WoundType } from 'src/app/models/WoundType';
import { ApiService } from 'src/app/services/api.service';
import { CreateWoundDto } from '../../DTOs/CreateWoundDto';
import { Router } from '@angular/router';
import { FastestAppointmentDto } from 'src/app/DTOs/FastestAppointmentDto';
import { CreateTreatmentWithAppointmentDto } from 'src/app/DTOs/CreateTreatmentWithAppointmentDto';
import { Location } from '@angular/common';

@Component({
  selector: 'app-register-wound',
  templateUrl: './register-wound.component.html',
  styleUrls: ['./register-wound.component.css']
})
export class RegisterWoundComponent implements OnInit {

  woundTypes: WoundType[] = [];
  woundLocations: WoundLocation[] = [];
  woundSizes: WoundSize[] = [];
  woundColors: WoundColor[] = [];
  woundOdors: WoundOdor[] = [];
  woundExudates: WoundExudate[] = [];
  woundBleedings: WoundBleeding[] = [];
  surroundingSkins: SurroundingSkin[] = [];
  painTypes: PainType[] = [];
  painLevels: PainLevel[] = [];

  isLinear = true;
  woundTypeFormGroup!: FormGroup;
  woundLocationFormGroup!: FormGroup;
  woundSizeFormGroup!: FormGroup;
  woundColorFormGroup!: FormGroup;
  woundOdorFormGroup!: FormGroup;
  woundExudateFormGroup!: FormGroup;
  woundBleedingFormGroup!: FormGroup;
  surroundingSkinFormGroup!: FormGroup;
  painTypeFormGroup!: FormGroup;
  painLevelFormGroup!: FormGroup;

  woundCreated = false;
  visitWanted = false;

  woundId: number | undefined = 0;
  fastestAppointment: FastestAppointmentDto | undefined = undefined;

  constructor(private _formBuilder: FormBuilder, private apiService: ApiService, private router: Router, private _location: Location) { }

  async goToMain(){
    this.router.navigateByUrl('/main');
  }

  async goBack() {
    this._location.back();
  }

  get woundType(): any {
    return this.woundTypeFormGroup.get('woundTypeCtrl');
  }
  get woundLocation(): any {
    return this.woundLocationFormGroup.get('woundLocationCtrl');
  }
  get woundSize(): any {
    return this.woundSizeFormGroup.get('woundSizeCtrl');
  }
  get woundColor(): any {
    return this.woundColorFormGroup.get('woundColorCtrl');
  }
  get woundOdor(): any {
    return this.woundOdorFormGroup.get('woundOdorCtrl');
  }
  get woundExudate(): any {
    return this.woundExudateFormGroup.get('woundExudateCtrl');
  }
  get woundBleeding(): any {
    return this.woundBleedingFormGroup.get('woundBleedingCtrl');
  }
  get surroundingSkin(): any {
    return this.surroundingSkinFormGroup.get('surroundingSkinCtrl');
  }
  get painType(): any {
    return this.painTypeFormGroup.get('painTypeCtrl');
  }
  get painLevel(): any {
    return this.painLevelFormGroup.get('painLevelCtrl');
  }

  async loadWoundTypes() {

    console.log('load wound types');

    // wound types
    this.woundTypes = await this.apiService.getWoundTypes();
    console.log('wound types: ', this.woundTypes);

    // wound locations
    this.woundLocations = await this.apiService.getWoundLocations();
    console.log('wound locations: ', this.woundLocations);

    // wound sizes
    this.woundSizes = await this.apiService.getWoundSizes();
    console.log('wound sizes: ', this.woundSizes);

    // wound colors
    this.woundColors = await this.apiService.getWoundColors();
    console.log('wound colors: ', this.woundColors);

    // wound odors
    this.woundOdors = await this.apiService.getWoundOdors();
    console.log('wound odors: ', this.woundOdors);

    // wound exudates
    this.woundExudates = await this.apiService.getWoundExudates();
    console.log('wound exudates: ', this.woundExudates);

    // wound bleedings
    this.woundBleedings = await this.apiService.getWoundBleedings();
    console.log('wound bleedings: ', this.woundBleedings);

    // surrounding skins
    this.surroundingSkins = await this.apiService.getSurroundingSkins();
    console.log('surrounding skins: ', this.surroundingSkins);

    // pain types
    this.painTypes = await this.apiService.getPainTypes();
    console.log('pain types: ', this.painTypes);

    // pain levels
    this.painLevels = await this.apiService.getPainLevels();
    console.log('pain levels: ', this.painLevels);

    console.log('loaded wound types');
  }

  async onRegister() {
    console.log('onRegister');

    // dto
    let createWoundDto: CreateWoundDto = {
      userId: this.apiService.loggedUserId,
      woundTypeId: this.woundType.value[0],
      woundLocationId: this.woundLocation.value[0],
      woundSizeId: this.woundSize.value[0],
      woundColorId: this.woundColor.value[0],
      woundOdorId: this.woundOdor.value[0],
      woundExudateId: this.woundExudate.value[0],
      woundBleedingId: this.woundBleeding.value[0],
      surroundingSkinId: this.surroundingSkin.value[0],
      painTypeId: this.painType.value[0],
      painLevelId: this.painLevel.value[0]
    };
    console.log('createWoundDto: ', createWoundDto);

    // api
    await this.apiService.createWound(createWoundDto);
    console.log('Wound created');
    this.woundCreated = true;

    // get wound id
    this.woundId = await this.apiService.getNewestWoundIdByUserId(this.apiService.loggedUserId);
    console.log('Created wound id: ', this.woundId);
  }

  async onCreateAppointment() {
    console.log('onCreateAppointment');
    this.visitWanted = true;

    // load fastest available appointment
    this.fastestAppointment = await this.apiService.getFastestAppointment();
  }

  async onCancel() {
    console.log('onCancel');
    this.router.navigateByUrl('/main');
  }

  async onApproveAppointment(){
    console.log('onApproveAppointment');
    
    // create treatment with apppointment
    let createTreatmentWithAppointmentDto: CreateTreatmentWithAppointmentDto = {
      woundId: this.woundId,
      doctorId: this.fastestAppointment?.doctorId,
      patientId: this.apiService.loggedUserId,
      appointmentDate: this.fastestAppointment?.appointmentDate
    }
    console.log('createTreatmentWithAppointmentDto: ', createTreatmentWithAppointmentDto);

    // api
    await this.apiService.createTreatmentWithAppointment(createTreatmentWithAppointmentDto);
    console.log('Treatment with Appointment created');
    this.router.navigateByUrl('/main');
  }

  async onOtherAppointment(){
    console.log('onOtherAppointment');
    this.router.navigateByUrl('/register-appointment');
  }

  ngOnInit(): void {
    // load wound types
    (async () => {
      await this.loadWoundTypes();
    })();

    this.woundTypeFormGroup = this._formBuilder.group({
      woundTypeCtrl: ['', Validators.required],
    });
    this.woundLocationFormGroup = this._formBuilder.group({
      woundLocationCtrl: ['', Validators.required],
    });
    this.woundSizeFormGroup = this._formBuilder.group({
      woundSizeCtrl: ['', Validators.required],
    });
    this.woundColorFormGroup = this._formBuilder.group({
      woundColorCtrl: ['', Validators.required],
    });
    this.woundOdorFormGroup = this._formBuilder.group({
      woundOdorCtrl: ['', Validators.required],
    });
    this.woundExudateFormGroup = this._formBuilder.group({
      woundExudateCtrl: ['', Validators.required],
    });
    this.woundBleedingFormGroup = this._formBuilder.group({
      woundBleedingCtrl: ['', Validators.required],
    });
    this.surroundingSkinFormGroup = this._formBuilder.group({
      surroundingSkinCtrl: ['', Validators.required],
    });
    this.painTypeFormGroup = this._formBuilder.group({
      painTypeCtrl: ['', Validators.required],
    });
    this.painLevelFormGroup = this._formBuilder.group({
      painLevelCtrl: ['', Validators.required],
    });
  }
}