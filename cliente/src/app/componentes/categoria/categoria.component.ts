import { Component, OnInit } from '@angular/core';
import { CategoriasService } from '../../servicios/categoria.service';

@Component({
  selector: 'app-categorias',
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.css']
})
export class CategoriasComponent implements OnInit {
  categorias: any[] = [];

  constructor(private categoriasService: CategoriasService) {}

  ngOnInit(): void {
    this.categoriasService.getCategorias().subscribe((data: any) => {
      this.categorias = data;
    });
  }
}
