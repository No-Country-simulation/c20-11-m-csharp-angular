import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { API_ENDPOINT } from '../../../../vars';
import { CommonModule } from '@angular/common';

export interface User {
  name: string;
  email: string;
}

@Component({
  selector: 'app-redirect-login',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive,CommonModule],
  templateUrl: './redirect-login.component.html',
  styleUrls: ['./redirect-login.component.css']
})
export class RedirectLoginComponent implements OnInit {
  title = 'redirect';
  user: User | null = null;
  isLoading: boolean = true;
  code: string | null = null;
  showRedirectButton: boolean = false;
  login_exitoso = 0;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.code = this.getCodeFromQueryParams();
    if (this.code) {
      this.loginUser();
    }
  }

  getCodeFromQueryParams(): string | null {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('code');
  }

  loginUser(): void {
    this.http.post(`${API_ENDPOINT}/users/login?code=${this.code}`, null, { withCredentials: true }).subscribe({
      next: (data: any) => {
        this.login_exitoso = 1;
        console.log('Datos completos:', data);
        localStorage.setItem("nombre", data.nombre);
        localStorage.setItem("id_user",data.usuarioID);
        localStorage.setItem('isAuthenticated', 'true');
      },
      error: (e) => {
        this.login_exitoso = 2;
        console.error('Error al obtener los datos de la API:', e);
        this.showRedirectButton = true;
      },
      complete: () => {
        console.info('Solicitud completada');
        this.showRedirectButton = true;
      }
    });
  }

  redirectToHome(): void {
    const userId = localStorage.getItem('id_user'); // Obtener el ID del usuario del local storage
    if (userId) {
      this.router.navigate(['/usuarios/', userId]); // Redirigir pasando el ID
    } else {
      this.router.navigate(['/']);
      console.error('No se encontró el ID del usuario en el local storage.');
    }
  }
}
