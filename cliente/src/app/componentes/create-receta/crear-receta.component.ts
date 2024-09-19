import { Component, HostListener, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TagInputModule } from 'ngx-chips';
import { API_ENDPOINT } from '../../../../vars';
import { TagFormComponent } from "../tag-form/tag-form.component";
import { ValidateForm } from '../../../utils/Form/form.validation';

@Component({
  selector: 'app-crear-receta',
  templateUrl: './crear-receta.component.html',
  styleUrls: ['./crear-receta.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule, TagInputModule, TagFormComponent]
})
export class CrearRecetaComponent {
  validationErrors: { [key: string]: string } = {};
  receta = {
    nombre: '',
    descripcion: '',
    imageUrl: '',
    tiempo_de_coccion:"0",
  };

  showModal = false;
  name_input = '';
  cantidad_input = '';
  items: any[] = [];

  list_c = [''];
  categorias = ['Postres', 'Desayuno', 'Aperitivos', 'Principales'];
  user_id = null;

  @ViewChild(TagFormComponent) tagFormComponent!: TagFormComponent;

  constructor(private http: HttpClient) {}

  // Método que valida y almacena errores
  validateField(event: Event) {
    const input = event.target as HTMLInputElement;
    const validationResult = ValidateForm(input);

    if (validationResult !== true) {
      this.validationErrors[input.name] = validationResult as string;
    } else {
      delete this.validationErrors[input.name];
    }
  }
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
        nombre: this.name_input,
        cantidad: this.cantidad_input,
        displayName: `${this.name_input} (${this.cantidad_input})`
      });
      console.log(this.items);
      
      this.closeModal();
    } else {
      alert('Por favor, completa ambos campos');
    }
  }

  //Mandar formulario
  @HostListener('document:keydown.enter', ['$event'])
  handleEnterKey(event: KeyboardEvent) {
    event.preventDefault();
  }
  onSubmit(event: KeyboardEvent) {
    event.preventDefault();
    const selectedCategories = this.tagFormComponent.items.map(item => item.value);

    const data = {
      receta: this.receta,
      list_i: this.items,
      list_c: selectedCategories,
      user_id: this.user_id
    };
    console.log(data);
    
    const confirmacion = window.confirm('¿Estás seguro de que quieres subir la receta?');

    if (confirmacion) {
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
    } else {
      // Si el usuario cancela, no hace nada
      console.log('El usuario canceló la subida de la receta.');
    }
  }
}
