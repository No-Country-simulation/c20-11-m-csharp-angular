import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VistaDetalleRecetaComponent } from './vista-detalle-receta.component';

describe('VistaDetalleRecetaComponent', () => {
  let component: VistaDetalleRecetaComponent;
  let fixture: ComponentFixture<VistaDetalleRecetaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VistaDetalleRecetaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VistaDetalleRecetaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
