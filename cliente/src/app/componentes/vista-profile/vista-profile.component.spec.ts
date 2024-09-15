import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VistaProfileComponent } from './vista-profile.component';

describe('VistaProfileComponent', () => {
  let component: VistaProfileComponent;
  let fixture: ComponentFixture<VistaProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VistaProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VistaProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
