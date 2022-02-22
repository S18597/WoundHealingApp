import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WoundDetailsComponent } from './wound-details.component';

describe('WoundDetailsComponent', () => {
  let component: WoundDetailsComponent;
  let fixture: ComponentFixture<WoundDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WoundDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WoundDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
