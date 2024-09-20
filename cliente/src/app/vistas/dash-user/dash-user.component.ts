import { Component, OnInit } from '@angular/core';
import { UserService } from '../../servicios/user.service';
import { RecetasService } from '../../servicios/receta.service'; 
import { Receta } from '../../model/receta.model'; 
import { Usuario } from '../../model/usuario.model';

@Component({
  selector: 'app-dash-user',
  templateUrl: './dash-user.component.html',
  styleUrls: ['./dash-user.component.css']
})
export class DashUserComponent implements OnInit {
  usuario: Usuario | null = null; // Información del usuario
  recetas: Receta[] = []; // Recetas creadas por el usuario
  isLoading = true; // Para manejar el estado de carga

  constructor(
    private userService: UserService,
    private recetaService: RecetasService
  ) {}

  ngOnInit(): void {
    const userId = localStorage.getItem('id_user');
    const userName = localStorage.getItem('nombre'); // Obtener el nombre del usuario
    if (userId && userName) {
      this.usuario = { id: Number(userId), nombre: userName }; // Establecer el nombre del usuario
      this.obtenerRecetas(); // Obtener las recetas del usuario
    } else {
      console.error('No se encontró el ID del usuario en el local storage.');
    }
  }

  // Obtener todas las recetas y filtrar por usuario
  obtenerRecetas(): void {
    this.recetaService.getRecetas().subscribe(
      (data: Receta[]) => {
        const userId = Number(localStorage.getItem('id_user'));
        this.recetas = data.filter(receta => receta.usuario.usuarioID === userId); // Filtrar recetas por ID del usuario
        this.isLoading = false; 
      },
      (error) => {
        console.error('Error al obtener recetas:', error);
        this.isLoading = false; 
      }
    );
  }
}

