import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentNotesComponent } from './appointment-notes.component';

describe('AppointmentNotesComponent', () => {
  let component: AppointmentNotesComponent;
  let fixture: ComponentFixture<AppointmentNotesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppointmentNotesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppointmentNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
