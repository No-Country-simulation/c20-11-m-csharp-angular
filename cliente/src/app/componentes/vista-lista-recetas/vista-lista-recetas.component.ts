import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecetasService } from '../../servicios/receta.service'; // Asegúrate de que la ruta sea correcta
import { Receta } from '../../model/receta.model';
import { CategoriasService } from '../../servicios/categoria.service';
import { Categoria } from '../../model/categoria.model';

@Component({
  selector: 'app-vista-lista-recetas',
  templateUrl: './vista-lista-recetas.component.html',
  styleUrls: ['./vista-lista-recetas.component.css']
})
export class VistaListaRecetasComponent implements OnInit {
  recetas: Receta[] = [];
  isLoading = true;
  categoriaId!: number;
  searchTerm: string = '';
  categoriaNombre: string = '';

  constructor(
    private route: ActivatedRoute,
    private recetasService: RecetasService,
    private categoriasService: CategoriasService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.categoriaId = +params['categoriaId'];
      this.searchTerm = params['q'] || '';

      if (this.categoriaId) {
        this.obtenerRecetasPorCategoria(this.categoriaId);
        this.obtenerCategoriaNombre(this.categoriaId);
      } else if (this.searchTerm) {
        this.obtenerRecetasPorBusqueda(this.searchTerm);
      }
    });
  }

  obtenerRecetasPorCategoria(categoriaId: number): void {
    this.recetasService.getRecetasPorCategoria(categoriaId).subscribe(
      (data: Receta[]) => {
        this.recetas = data;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error al obtener recetas por categoría:', error);
        this.isLoading = false;
      }
    );
  }

  obtenerRecetasPorBusqueda(query: string): void {
    this.recetasService.getRecetasPorBusqueda(query).subscribe(
      (data: Receta[]) => {
        this.recetas = data;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error al buscar recetas:', error);
        this.isLoading = false;
      }
    );
  }

  obtenerCategoriaNombre(categoriaId: number): void {
    this.categoriasService.getCategorias().subscribe(
      (data: Categoria[]) => {
        const categoria = data.find(cat => cat.categoriaID === categoriaId);
        this.categoriaNombre = categoria ? categoria.nombre : 'Categoría no encontrada';
      },
      (error) => {
        console.error('Error al obtener categoría:', error);
      }
    );
  }
}
