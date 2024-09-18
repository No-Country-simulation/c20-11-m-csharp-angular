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
    this.recetasService.getRecetasOrdenadas().subscribe(
      (data: Receta[]) => {
        console.log('Datos recibidos en el componente:', data); // Agrega este log
        this.recetasDelDia = data;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error al obtener recetas:', error);
        this.isLoading = false;
      }
    );
  }}