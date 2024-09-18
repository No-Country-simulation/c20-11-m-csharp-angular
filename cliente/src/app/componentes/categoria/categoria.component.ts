import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'; // Importar Router
import { Categoria } from '../../model/categoria.model'; // Asegúrate de que este modelo esté definido
import { CategoriasService } from '../../servicios/categoria.service';

@Component({
  selector: 'app-categorias',
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.css']
})
export class CategoriasComponent implements OnInit {
  categorias: Categoria[] = []; // Lista para almacenar las categorías

  constructor(private categoriasService: CategoriasService, private router: Router) {}

  ngOnInit(): void {
    this.categoriasService.getCategorias().subscribe(
      (data: Categoria[]) => {
        this.categorias = data;
      },
      (error) => {
        console.error('Error al obtener categorías:', error);
      }
    );
  }

  verRecetas(categoriaId: number): void {
    this.router.navigate(['/resultados'], { queryParams: { categoriaId: categoriaId } });

  }
}
