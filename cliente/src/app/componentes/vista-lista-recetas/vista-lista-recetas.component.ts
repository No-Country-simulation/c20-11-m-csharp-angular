import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecetasService } from '../../servicios/receta.service';
import { CategoriasService } from '../../servicios/categoria.service';
import { Receta } from '../../model/receta.model';
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
  categoriaNombre: string = ''; // Para almacenar el nombre de la categoría

  constructor(
    private route: ActivatedRoute,
    private recetasService: RecetasService,
    private categoriasService: CategoriasService // Inyección del servicio de categorías
  ) {}

  ngOnInit(): void {
    // Obtener el parametro de la URL
    this.route.queryParams.subscribe(params => {
      this.categoriaId = +params['categoriaId']; // El '+' convierte el string a número
      if (this.categoriaId) {
        this.obtenerRecetasPorCategoria(this.categoriaId);
        this.obtenerNombreCategoria(this.categoriaId);
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

  obtenerNombreCategoria(categoriaId: number): void {
    this.categoriasService.getCategorias().subscribe(
      (data: Categoria[]) => {
        const categoria = data.find(cat => cat.categoriaID === categoriaId);
        if (categoria) {
          this.categoriaNombre = categoria.nombre;
        }
      },
      (error) => {
        console.error('Error al obtener categorías:', error);
      }
    );
  }
}

// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
// import { RecetasService } from '../../servicios/receta.service'; // Asegúrate de que la ruta sea correcta
// import { Receta } from '../../model/receta.model';

// @Component({
//   selector: 'app-vista-lista-recetas',
//   templateUrl: './vista-lista-recetas.component.html',
//   styleUrls: ['./vista-lista-recetas.component.css']
// })
// export class VistaListaRecetasComponent implements OnInit {
//   recetas: Receta[] = [];
//   isLoading = true;
//   categoriaNombre: string = '';
//   categoriaId!: number;

//   constructor(
//     private route: ActivatedRoute,
//     private recetasService: RecetasService
//   ) {}

//   ngOnInit(): void {
//     // Obtener el parámetro de la URL
//     this.route.queryParams.subscribe(params => {
//       this.categoriaId = +params['categoriaId']; // El '+' convierte el string a número
//       if (this.categoriaId) {
//         this.obtenerRecetasPorCategoria(this.categoriaId);
//       }
//     });
//   }

//   obtenerRecetasPorCategoria(categoriaId: number): void {
//     this.recetasService.getRecetasPorCategoria(categoriaId).subscribe(
//       (data) => {
//         this.recetas = data.recetas;
//         this.categoriaNombre = data.nombre; // Asigna el nombre de la categoría
//         this.isLoading = false;
//       },
//       (error) => {
//         console.error('Error al obtener recetas por categoría:', error);
//         this.isLoading = false;
//       }
//     );
//   }
// }
