import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalificacionStarComponent } from './calificacion-star.component';

describe('CalificacionStarComponent', () => {
  let component: CalificacionStarComponent;
  let fixture: ComponentFixture<CalificacionStarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CalificacionStarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CalificacionStarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
