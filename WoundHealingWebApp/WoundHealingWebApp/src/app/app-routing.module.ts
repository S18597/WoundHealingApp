import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuardService } from './services/auth-guard.service';
import { HomeComponent } from './components/home/home.component';
import { CreateAccountComponent } from './components/create-account/create-account.component';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { RegisterWoundComponent } from './components/register-wound/register-wound.component';
import { MyWoundsComponent } from './components/my-wounds/my-wounds.component';
import { RegisterAppointmentComponent } from './components/register-appointment/register-appointment.component';
import { MyAppointmentsComponent } from './components/my-appointments/my-appointments.component';
import { ChatComponent } from './components/chat/chat.component';
import { PatientDataComponent } from './components/patient-data/patient-data.component';
import { AppointmentComponent } from './components/appointment/appointment.component';
import { WoundDetailsComponent } from './components/wound-details/wound-details.component';
import { VideoCallComponent } from './components/video-call/video-call.component';
import { WebrtcTestComponent } from './components/webrtc-test/webrtc-test.component';
import { MyStatsComponent } from './components/my-stats/my-stats.component';
import { MyPatientsComponent } from './components/my-patients/my-patients.component';
import { PatientWoundsComponent } from './components/patient-wounds/patient-wounds.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    data: { title: 'WoundHealingApp: Welcome' }
  },
  {
    path: 'create',
    component: CreateAccountComponent,
    data: { title: 'WoundHealingApp: Create an account' }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'WoundHealingApp: Login' }
  },
  {
    path: 'main',
    component: MainComponent,
    data: { title: 'WoundHealingApp: MainPage' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'register-wound',
    component: RegisterWoundComponent,
    data: { title: 'WoundHealingApp: Register wound' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'my-wounds',
    component: MyWoundsComponent,
    data: { title: 'WoundHealingApp: My wounds' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'register-appointment',
    component: RegisterAppointmentComponent,
    data: { title: 'WoundHealingApp: Register appointment' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'my-appointments',
    component: MyAppointmentsComponent,
    data: { title: 'WoundHealingApp: My appointments' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'chat',
    component: ChatComponent,
    data: { title: 'WoundHealingApp: Chat' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'patient-data',
    component: PatientDataComponent,
    data: { title: 'WoundHealingApp: Patient data' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'appointment/:appointmentId',
    component: AppointmentComponent,
    data: { title: 'WoundHealingApp: Appointment' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'wound-details/:woundId',
    component: WoundDetailsComponent,
    data: { title: 'WoundHealingApp: Wound Details' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'video-call',
    component: VideoCallComponent,
    data: { title: 'WoundHealingApp: Video call' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'my-stats',
    component: MyStatsComponent,
    data: { title: 'WoundHealingApp: My stats' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'my-patients',
    component: MyPatientsComponent,
    data: { title: 'WoundHealingApp: My patients' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'patient-wounds',
    component: PatientWoundsComponent,
    data: { title: 'WoundHealingApp: My patients wounds' },
    canActivate: [AuthGuardService]
  },
  {
    path: 'webrtc-test',
    component: WebrtcTestComponent,
    data: { title: 'WoundHealingApp: WebRTC test' },
    canActivate: [AuthGuardService]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }