import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RedirectRegisterComponent } from './redirect-register.component';

describe('RedirectRegisterComponent', () => {
  let component: RedirectRegisterComponent;
  let fixture: ComponentFixture<RedirectRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RedirectRegisterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RedirectRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
