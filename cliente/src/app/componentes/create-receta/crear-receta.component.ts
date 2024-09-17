import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TagInputModule } from 'ngx-chips';
import { API_ENDPOINT } from '../../../../vars';

@Component({
  selector: 'app-crear-receta',
  templateUrl: './crear-receta.component.html',
  styleUrls: ['./crear-receta.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, TagInputModule]
})
export class CrearRecetaComponent {
  receta = {
    nombre: '',
    descripcion: '',
    imageUrl: ''
  };
  showModal = false;
  name_input = '';
  cantidad_input = '';
  items: any[] = [];

  list_c = [''];
  categorias = ['Postres', 'Desayuno', 'Aperitivos', 'Principales'];
  user_id = null;

  constructor(private http: HttpClient) {}

  onAdd(event: any) {
    this.name_input = event.displayName
    this.items = this.items.filter(item => item.displayName !== event.displayName);
    this.openModal();
  }

  onRemove(event: any) {
    const index = this.items.findIndex(item => item.displayName === event.displayName);
    if (index >= 0) {
      this.items.splice(index, 1);
    }
  }

  openModal() {

    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.name_input = "";
    this.cantidad_input = "";
  }

  addIngrediente() {
    if (this.name_input && this.cantidad_input) {
      this.items.push({
        name: this.name_input,
        cantidad: this.cantidad_input,
        displayName: `${this.name_input} (${this.cantidad_input})`
      });
      this.closeModal();
    } else {
      alert('Por favor, completa ambos campos');
    }
  }

  // Enviar los datos del formulario
  onSubmit() {
    const data = {
      receta: this.receta,
      ingredientes: this.items,
      list_c: this.list_c,
      user_id: this.user_id
    };
    
    this.http.post(`${API_ENDPOINT}/api/receta`, data, { withCredentials: true })
      .subscribe({
        next: (data: any) => {
          console.log('Datos enviados:', data);
        },
        error: (e) => {
          console.error('Error al enviar los datos:', e);
        },
        complete: () => {
          console.info('Solicitud completada');
        }
      });
  }
}
