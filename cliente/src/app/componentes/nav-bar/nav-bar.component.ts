import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/Login/AuthService';  // Ajusta la ruta según corresponda
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  isAuthenticated = false;

  constructor(private router: Router) {} // Inyectar el Router

  ngOnInit(): void {
    this.isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';
  }

  logout(): void {
    localStorage.removeItem('isAuthenticated'); 
    localStorage.removeItem('nombre'); 
    localStorage.removeItem('id_user'); // También puedes eliminar el ID del usuario
    this.isAuthenticated = false;
  }

  redirectToUserDashboard(): void {
    const userId = localStorage.getItem('id_user'); // Obtener el ID del usuario del local storage
    if (userId) {
      this.router.navigate(['/usuarios/', userId]); // Redirigir pasando el ID
    } else {
      console.error('No se encontró el ID del usuario en el local storage.');
    }
  }
}
