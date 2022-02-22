import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { User } from '../models/User';
import { Auth } from '../models/Auth';
import { createUserDto } from '../DTOs/createUserDto';
import { WoundType } from '../models/WoundType';
import { WoundLocation } from '../models/WoundLocation';
import { WoundSize } from '../models/WoundSize';
import { WoundColor } from '../models/WoundColor';
import { WoundOdor } from '../models/WoundOdor';
import { WoundExudate } from '../models/WoundExudate';
import { WoundBleeding } from '../models/WoundBleeding';
import { SurroundingSkin } from '../models/SurroundingSkin';
import { PainType } from '../models/PainType';
import { PainLevel } from '../models/PainLevel';
import { CreateWoundDto } from '../DTOs/CreateWoundDto';
import { FastestAppointmentDto } from '../DTOs/FastestAppointmentDto';
import { CreateTreatmentWithAppointmentDto } from '../DTOs/CreateTreatmentWithAppointmentDto';
import { MyWoundDto } from '../DTOs/MyWoundDto';
import { FindAppointmentDto } from '../DTOs/FindAppointmentDto';
import { DoctorDto } from '../DTOs/DoctorDto';
import { CreateAppointmentDto } from '../DTOs/CreateAppointmentDto';
import { MyAppointmentDto } from '../DTOs/MyAppointmentDto';
import { MyAppointmentDocDto } from '../DTOs/MyAppointmentDocDto';
import { WoundAppointmentDto } from '../DTOs/WoundAppointmentDto';
import { MyMedicalDataDto } from '../DTOs/MyMedicalDataDto';
import { MedicalDataDto } from '../DTOs/MedicalDataDto';
import { AppointmentSummaryDto } from '../DTOs/AppointmentSummaryDto';
import { FileUploadDto } from '../DTOs/FileUploadDto';
import { WoundPhotoDetailDto } from '../DTOs/WoundPhotoDetailDto';
import { AppointmentNoteDto } from '../DTOs/AppointmentNoteDto';
import { ChatDto } from '../DTOs/ChatDto';
import { MessageDto } from '../DTOs/MessageDto';
import { StatsDto } from '../DTOs/StatsDto';
import { MyPatientDto } from '../DTOs/MyPatientDto';
import { MyPatientWoundDto } from '../DTOs/MyPatientWoundDto';
import { FinishTreatmentDto } from '../DTOs/FinishTreatmentDto';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  isLogged = false;
  loggedUserId: number | undefined;
  loggedUserEmail!: string;
  loggedUserIsPatient: boolean | undefined = false;
  loggedUserIsDoctor: boolean | undefined =  false;

  doctorsSelectedPatientId!: number;
  doctorsSelectedPatientEmail!: string

  config: any;
  apiUrl: string | undefined;

  constructor(private http: HttpClient) { }

  init() {
    this.http.get('assets/config.json').subscribe((c: any) => {
      console.log('got config:', c);
      this.config = c;
      this.apiUrl = c.apiUrl;
    });
  }

  async createUser(userData: createUserDto): Promise<any> {
    const url = this.apiUrl + 'User/CreateUser';
    console.log(`url: ${url}, data: `, userData);
    return await this.http.post<createUserDto>(url, userData).toPromise();
  }

  async createWound(woundData: CreateWoundDto): Promise<any> {
    const url = this.apiUrl + 'Wound/CreateWound';
    console.log(`url: ${url}, data: `, woundData);
    return await this.http.post<CreateWoundDto>(url, woundData).toPromise();
  }

  async createAppointment(createAppointmentDto: CreateAppointmentDto): Promise<any> {
    const url = this.apiUrl + 'Appointment/CreateAppointment';
    console.log(`url: ${url}, data: `, createAppointmentDto);
    return await this.http.post<CreateAppointmentDto>(url, createAppointmentDto).toPromise();
  }

  async saveMedicalData(medicalDataDto: MedicalDataDto): Promise<any> {
    const url = this.apiUrl + 'MedicalData/SaveMedicalData';
    console.log(`url: ${url}, data: `, medicalDataDto);
    return await this.http.post<CreateAppointmentDto>(url, medicalDataDto).toPromise();
  }

  async createTreatmentWithAppointment(treatmentAppointmentData: CreateTreatmentWithAppointmentDto): Promise<any> {
    const url = this.apiUrl + 'Appointment/CreateTreatmentWithAppointment';
    console.log(`url: ${url}, data: `, treatmentAppointmentData);
    return await this.http.post<CreateTreatmentWithAppointmentDto>(url, treatmentAppointmentData).toPromise();
  }

  async saveAppointmentNote(appointmentNoteDto: AppointmentNoteDto) {
    const url = this.apiUrl + 'Appointment/SaveAppointmentNote';
    console.log(`url: ${url}, data: `, appointmentNoteDto);
    return await this.http.post<AppointmentNoteDto>(url, appointmentNoteDto).toPromise();
  }

  async uploadPhoto(fileUploadDto: FileUploadDto): Promise<any> {

    const uploadData = new FormData();
    uploadData.append('file', fileUploadDto.file);
    uploadData.append('filename', fileUploadDto.filename);

    const url = this.apiUrl + 'Wound/UploadWoundPhoto/' + fileUploadDto.woundId;
    console.log(`url: ${url}, data: `, fileUploadDto);

    return await this.http.post<File>(url, uploadData).toPromise();
  }

  async finishTreatment(finishTreatmentDto: FinishTreatmentDto): Promise<any> {
    const url = this.apiUrl + 'Wound/FinishTreatment';
    console.log(`url: ${url}, data: `, finishTreatmentDto);
    return await this.http.post<FinishTreatmentDto>(url, finishTreatmentDto).toPromise();
  }

  async getPhoto(woundPhotoId: number): Promise<any> {
    const url = this.apiUrl + 'Wound/GetWoundPhoto/' + woundPhotoId;
    console.log(`url: ${url}`);
    let blob = this.getFileBlob(url);
    console.log('getFileBlob observable: ', blob);
    return blob.toPromise();
    //return await this.http.get<any>(url).toPromise();
  }

  async getPhoto2(woundPhotoId: number): Promise<any> {
    const url = this.apiUrl + 'Wound/GetWoundPhoto2/' + woundPhotoId;
    console.log(`url: ${url}`);
    return await this.http.get<any>(url).toPromise();
  }

  async getAppointmentNote(appointmentId: string | null): Promise<AppointmentNoteDto> {
    const url = this.apiUrl + 'Appointment/GetAppointmentNote/' + appointmentId;
    console.log(`url: ${url}`);
    return await this.http.get<AppointmentNoteDto>(url).toPromise();
  }

  async getWoundPhotoDetails(woundId: string | null): Promise<WoundPhotoDetailDto[]> {
    const url = this.apiUrl + 'Wound/GetWoundPhotoDetails/' + woundId;
    console.log(`url: ${url}`);
    return await this.http.get<WoundPhotoDetailDto[]>(url).toPromise();
  }

  async getDoctorStats(doctorId: number | undefined): Promise<StatsDto> {
    const url = this.apiUrl + 'User/GetDoctorStats/' + doctorId;
    console.log(`url: ${url}`);
    return await this.http.get<StatsDto>(url).toPromise();
  }

  async getMyPatients(doctorId: number | undefined): Promise<MyPatientDto[]> {
    const url = this.apiUrl + 'User/GetMyPatients/' + doctorId;
    console.log(`url: ${url}`);
    return await this.http.get<MyPatientDto[]>(url).toPromise();
  }

  async getMyPatientsWounds(doctorId: number | undefined): Promise<MyPatientWoundDto[]> {
    const url = this.apiUrl + 'User/GetMyPatientsWounds/' + doctorId;
    console.log(`url: ${url}`);
    return await this.http.get<MyPatientWoundDto[]>(url).toPromise();
  }


  async getChats(userId: number | undefined, isPatient: boolean | undefined): Promise<ChatDto[]> {
    const url = this.apiUrl + 'Chat/GetChats/' + userId + "/" + isPatient;
    console.log(`url: ${url}`);
    return await this.http.get<ChatDto[]>(url).toPromise();
  }

  async getChatMessages(chatId: number | undefined, isPatient: boolean | undefined): Promise<MessageDto[]> {
    const url = this.apiUrl + 'Chat/GetChatMessages/' + chatId + "/" + isPatient;
    console.log(`url: ${url}`);
    return await this.http.get<MessageDto[]>(url).toPromise();
  }

  async sendMessage(message: MessageDto): Promise<any> {
    const url = this.apiUrl + 'Chat/Message';
    console.log(`url: ${url}, data: `, message);
    return await this.http.post<MessageDto>(url, message).toPromise();
  }

  async deleteAppointment(appointmentId: number): Promise<any> {
    const url = this.apiUrl + 'Appointment/DeleteAppointment/' + appointmentId;
    console.log(`url: ${url}`);
    return await this.http.post<any>(url, null).toPromise();
  }

  async deleteWound(woundId: number): Promise<any> {
    const url = this.apiUrl + 'Wound/DeleteWound/' + woundId;
    console.log(`url: ${url}`);
    return await this.http.post<any>(url, null).toPromise();
  }

  async getNewestWoundIdByUserId(userId: number | undefined): Promise<any> {
    const url = this.apiUrl + 'Wound/GetNewestWoundIdByUserId/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<any>(url).toPromise();
  }

  async getMyAppointments(userId: number | undefined): Promise<MyAppointmentDto[]> {
    const url = this.apiUrl + 'Appointment/GetMyAppointments/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<MyAppointmentDto[]>(url).toPromise();
  }

  async getMyAppointmentsDoc(userId: number | undefined): Promise<MyAppointmentDocDto[]> {
    const url = this.apiUrl + 'Appointment/GetMyAppointmentsDoc/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<MyAppointmentDocDto[]>(url).toPromise();
  }
  
  async getUserByEmail(email: string): Promise<User> {
    const url = this.apiUrl + 'User/GetUserByEmail/' + email;
    console.log(`url: ${url}`);
    return await this.http.get<User>(url).toPromise();
  }

  async getUserAuthByUserId(userId: number | undefined): Promise<Auth> {
    const url = this.apiUrl + 'User/GetUserAuthByUserId/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<Auth>(url).toPromise();
  }

  async getFastestAppointment(): Promise<FastestAppointmentDto> {
    const url = this.apiUrl + 'Appointment/GetFastestAppointment';
    console.log(`url: ${url}`);
    return await this.http.get<FastestAppointmentDto>(url).toPromise();
  }

  async getAppointmentsPerDate(findAppointmentDto: FindAppointmentDto): Promise<FastestAppointmentDto[]> {
    const url = this.apiUrl + 'Appointment/GetAppointmentsPerDate';
    console.log(`url: ${url}`);
    return await this.http.post<FastestAppointmentDto[]>(url, findAppointmentDto).toPromise();
  }

  async getAppointmentSummery(appointmentId: string | null): Promise<AppointmentSummaryDto> {
    const url = this.apiUrl + 'Appointment/GetAppointmentSummary/' + appointmentId;
    console.log(`url: ${url}`);
    return await this.http.get<AppointmentSummaryDto>(url).toPromise();
  }

  async getDoctors(): Promise<DoctorDto[]> {
    const url = this.apiUrl + 'User/GetDoctors';
    console.log(`url: ${url}`);
    return await this.http.get<DoctorDto[]>(url).toPromise();
  }

  async getMyWounds(userId: number | undefined): Promise<MyWoundDto[]> {
    const url = this.apiUrl + 'Wound/GetMyWounds/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<MyWoundDto[]>(url).toPromise();
  }

  async getWoundDetails(woundId: string | null): Promise<MyWoundDto> {
    const url = this.apiUrl + 'Wound/GetWoundDetails/' + woundId;
    console.log(`url: ${url}`);
    return await this.http.get<MyWoundDto>(url).toPromise();
  }

  async getMyMedicalData(userId: number | undefined): Promise<MyMedicalDataDto> {
    const url = this.apiUrl + 'MedicalData/GetMedicalData/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<MyMedicalDataDto>(url).toPromise();
  }

  async getMyWoundAppointments(userId: number | undefined): Promise<WoundAppointmentDto[]> {
    const url = this.apiUrl + 'Wound/GetMyWoundAppointments/' + userId;
    console.log(`url: ${url}`);
    return await this.http.get<WoundAppointmentDto[]>(url).toPromise();
  }

  async getWoundTypes(): Promise<WoundType[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundTypes';
    console.log(`url: ${url}`);
    return await this.http.get<WoundType[]>(url).toPromise();
  }

  async getWoundLocations(): Promise<WoundLocation[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundLocations';
    console.log(`url: ${url}`);
    return await this.http.get<WoundLocation[]>(url).toPromise();
  }

  async getWoundSizes(): Promise<WoundSize[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundSizes';
    console.log(`url: ${url}`);
    return await this.http.get<WoundSize[]>(url).toPromise();
  }

  async getWoundColors(): Promise<WoundColor[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundColors';
    console.log(`url: ${url}`);
    return await this.http.get<WoundColor[]>(url).toPromise();
  }

  async getWoundOdors(): Promise<WoundOdor[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundOdors';
    console.log(`url: ${url}`);
    return await this.http.get<WoundOdor[]>(url).toPromise();
  }

  async getWoundExudates(): Promise<WoundExudate[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundExudates';
    console.log(`url: ${url}`);
    return await this.http.get<WoundExudate[]>(url).toPromise();
  }

  async getWoundBleedings(): Promise<WoundBleeding[]> {
    const url = this.apiUrl + 'WoundTypes/GetWoundBleedings';
    console.log(`url: ${url}`);
    return await this.http.get<WoundBleeding[]>(url).toPromise();
  }

  async getSurroundingSkins(): Promise<SurroundingSkin[]> {
    const url = this.apiUrl + 'WoundTypes/GetSurroundingSkins';
    console.log(`url: ${url}`);
    return await this.http.get<SurroundingSkin[]>(url).toPromise();
  }

  async getPainTypes(): Promise<PainType[]> {
    const url = this.apiUrl + 'WoundTypes/GetPainTypes';
    console.log(`url: ${url}`);
    return await this.http.get<PainType[]>(url).toPromise();
  }

  async getPainLevels(): Promise<PainLevel[]> {
    const url = this.apiUrl + 'WoundTypes/GetPainLevels';
    console.log(`url: ${url}`);
    return await this.http.get<PainLevel[]>(url).toPromise();
  }

  getFileBlob(url: string): Observable<Blob> {
    return this.http.get<Blob>(url, { observe: 'body', responseType: 'blob' as 'json' });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}