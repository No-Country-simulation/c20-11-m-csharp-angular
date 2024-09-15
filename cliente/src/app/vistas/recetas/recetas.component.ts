// src/app/recetas/recetas.component.ts
import { Component, OnInit } from '@angular/core';
import { RecetasService } from '../../servicios/receta.service';
@Component({
  selector: 'app-recetas',
  templateUrl: './recetas.component.html',
  styleUrls: ['./recetas.component.css']
})
export class RecetasComponent implements OnInit {
  recetas: any[] = [];
  isLoading: boolean = true;
  errorMessage: string = '';

  constructor(private recetasService: RecetasService) {}

  ngOnInit(): void {
    this.recetasService.getRecetas().subscribe({
      next: (data) => {
        this.recetas = data;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error al cargar las recetas';
        this.isLoading = false;
      }
    });
  }
}
