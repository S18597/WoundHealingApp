import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyWoundsComponent } from './my-wounds.component';

describe('MyWoundsComponent', () => {
  let component: MyWoundsComponent;
  let fixture: ComponentFixture<MyWoundsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyWoundsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyWoundsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
