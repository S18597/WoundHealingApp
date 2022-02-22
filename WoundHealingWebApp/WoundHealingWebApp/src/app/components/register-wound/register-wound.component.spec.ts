import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterWoundComponent } from './register-wound.component';

describe('RegisterWoundComponent', () => {
  let component: RegisterWoundComponent;
  let fixture: ComponentFixture<RegisterWoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterWoundComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterWoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
