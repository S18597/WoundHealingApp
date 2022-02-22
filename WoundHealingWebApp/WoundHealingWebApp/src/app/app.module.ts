import { forwardRef, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './modules/material/material.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';

import { AuthGuardService } from '../app/services/auth-guard.service';
import { ApiService } from '../app/services/api.service';
import { CreateAccountComponent } from './components/create-account/create-account.component';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { RegisterWoundComponent } from './components/register-wound/register-wound.component';
import { MyWoundsComponent } from './components/my-wounds/my-wounds.component';
import { RegisterAppointmentComponent } from './components/register-appointment/register-appointment.component';
import { MyAppointmentsComponent } from './components/my-appointments/my-appointments.component';
import { ChatComponent } from './components/chat/chat.component';
import { WebrtcTestComponent } from './components/webrtc-test/webrtc-test.component';
import { AngularFireModule } from "@angular/fire/compat";
import { AngularFireDatabaseModule } from '@angular/fire/compat/database';
import { environment } from 'src/environments/environment';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HatoolLibModule } from 'hatool';
import { PatientDataComponent } from './components/patient-data/patient-data.component';
import { AppointmentComponent } from './components/appointment/appointment.component';
import { WoundDetailsComponent } from './components/wound-details/wound-details.component';
import { VideoCallComponent } from './components/video-call/video-call.component';
import { AppointmentNotesComponent } from './components/appointment-notes/appointment-notes.component';
import { MyStatsComponent } from './components/my-stats/my-stats.component';
import { MyPatientsComponent } from './components/my-patients/my-patients.component';
import { PatientWoundsComponent } from './components/patient-wounds/patient-wounds.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CreateAccountComponent,
    LoginComponent,
    MainComponent,
    RegisterWoundComponent,
    MyWoundsComponent,
    RegisterAppointmentComponent,
    MyAppointmentsComponent,
    ChatComponent,
    WebrtcTestComponent,
    PatientDataComponent,
    AppointmentComponent,
    WoundDetailsComponent,
    VideoCallComponent,
    AppointmentNotesComponent,
    MyStatsComponent,
    MyPatientsComponent,
    PatientWoundsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireDatabaseModule,
    FlexLayoutModule,
    HatoolLibModule
  ],
  providers: [
    ApiService,
    AuthGuardService,
    // { provide: NG_VALUE_ACCESSOR,
    //   useExisting: forwardRef(() => MyInputField),
    //   multi: true
    // }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }