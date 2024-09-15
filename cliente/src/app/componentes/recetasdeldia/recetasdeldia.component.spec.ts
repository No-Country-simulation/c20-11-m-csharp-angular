import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecetasdeldiaComponent } from './recetasdeldia.component';

describe('RecetasdeldiaComponent', () => {
  let component: RecetasdeldiaComponent;
  let fixture: ComponentFixture<RecetasdeldiaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RecetasdeldiaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RecetasdeldiaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
