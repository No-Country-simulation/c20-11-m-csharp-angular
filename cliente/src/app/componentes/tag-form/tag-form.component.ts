import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TagInputModule } from 'ngx-chips';
import { API_ENDPOINT } from '../../../../vars';

interface Categoria {
  id: number;
  nombre: string;
}

@Component({
  selector: 'tag-form-component',
  templateUrl: './tag-form.component.html',
  styleUrl: './tag-form.component.css',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, TagInputModule]
})
export class TagFormComponent implements OnInit{
  receta = {
    nombre: '',
    descripcion: '',
    imageUrl: ''
  };

  items: any[] = [];
  categorias = ['Postres', 'Desayuno', 'Aperitivos', 'Principales'];
  filteredCategorias: any[] = [...this.categorias];

  constructor(private http: HttpClient) {}
  ngOnInit(): void {
    this.http.get<Categoria[]>(`${API_ENDPOINT}/api/categorias/all`)
      .subscribe({
        next: (response: Categoria[]) => {
          this.filteredCategorias = response.map(categoria => categoria.nombre) || [];
        },
        error: (error) => {
          console.error('Error fetching suggestions', error);
          this.filteredCategorias = [...this.categorias];
        }
      });
  }
  onAdd(event: any) {
    console.log('Etiqueta agregada:', event);
    console.log(this.items);
    
  }

  onRemove(event: any) {
    const index = this.items.findIndex(item => item.displayName === event.displayName);
    if (index >= 0) {
      this.items.splice(index, 1);
    }
  }
}