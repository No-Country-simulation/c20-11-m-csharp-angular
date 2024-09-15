import { Component, OnInit } from '@angular/core';
import { Receta } from '../../model/receta.model';
import { RecetasService } from '../../servicios/receta.service'; // Asegúrate de que esta ruta sea correcta.

@Component({
  selector: 'app-recetas-del-dia',
  templateUrl: './recetasdeldia.component.html',
  styleUrls: ['./recetasdeldia.component.css']
})
export class RecetasDelDiaComponent implements OnInit {
  recetasDelDia: Receta[] = []; // Lista para almacenar las recetas del día
  isLoading = true; // Variable para manejar el estado de carga

  constructor(private recetasService: RecetasService) {}

  ngOnInit(): void {
    // Llamada para obtener las recetas ordenadas por calificación
    this.recetasService.getRecetasOrdenadas().subscribe(
      (data: Receta[]) => {
        this.recetasDelDia = data;
        this.isLoading = false; // Desactiva el estado de carga
      },
      (error) => {
        console.error('Error al obtener recetas:', error);
        this.isLoading = false; // Desactiva el estado de carga incluso en caso de error
      }
    );
  }
}