import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { API_ENDPOINT } from '../../../../vars';

@Component({
  selector: 'app-crear-receta',
  templateUrl: './crear-receta.component.html',
  styleUrls: ['./crear-receta.component.css'],
  standalone:true,
  imports:[FormsModule,CommonModule]
})
export class CrearRecetaComponent {
  receta = {
    nombre: '',
    descripcion: '',
    imageUrl: ''
  };
  
  list_c = [''];
  categorias = ['Postres', 'Desayuno', 'Aperitivos', 'Principales'];
  user_id = null;

  constructor(private http: HttpClient) {}

  onSubmit() {
    const data = {
      receta: this.receta,
      list_c: this.list_c,
      user_id: this.user_id
    };
    
    this.http.post(`${API_ENDPOINT}/api/receta`, data,{withCredentials:true})
    .subscribe({
      next: (data: any) => {
        console.log('Datos completos:', data);
      },
      error: (e) => {
        console.log(data);
        console.error('Error al obtener los datos de la API:', e);
      },
      complete: () => {
        console.info('Solicitud completada');
      }
    });
  }
}
