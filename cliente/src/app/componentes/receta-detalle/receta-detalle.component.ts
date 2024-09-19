import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecetasService } from '../../servicios/receta.service'; // Asegúrate de que la ruta sea correcta
import { Receta } from '../../model/receta.model';

@Component({
  selector: 'app-receta-detalle',
  templateUrl: './receta-detalle.component.html',
  styleUrls: ['./receta-detalle.component.css']
})
export class RecetaDetalleComponent implements OnInit {
  receta: Receta | null = null; // Inicializa la receta

  constructor(
    private route: ActivatedRoute,
    private recetasService: RecetasService
  ) {}

  ngOnInit(): void {
    const recetaID = this.route.snapshot.paramMap.get('id');
    if (recetaID) {
      this.getRecetaById(recetaID);
    }
  }

  getRecetaById(id: string): void {
    this.recetasService.getRecetaById(id).subscribe(
      (data: Receta) => {
        this.receta = data;
      },
      (error) => {
        console.error('Error al obtener la receta:', error);
      }
    );
  }

  
}
