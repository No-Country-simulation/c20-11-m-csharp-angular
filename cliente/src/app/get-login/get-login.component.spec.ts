import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetLoginComponent } from './get-login.component';

describe('GetLoginComponent', () => {
  let component: GetLoginComponent;
  let fixture: ComponentFixture<GetLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetLoginComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GetLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
