import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { API_ENDPOINT } from '../../../../vars';
import { CommonModule } from '@angular/common';

interface User {
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
    this.http.post(`${API_ENDPOINT}/users/register?code=${this.code}`, null, { withCredentials: true }).subscribe({
      next: (data: any) => {
        console.log('Datos completos:', data);
        localStorage.setItem("nombre", data.nombre);
        localStorage.setItem('isAuthenticated', 'true');
        if (data && data.user) {
          this.user = data.user;
        } else {
          console.warn('La propiedad "user" no está presente en los datos recibidos');
        }
      },
      error: (e) => {
        console.error('Error al obtener los datos de la API:', e);
      },
      complete: () => {
        console.info('Solicitud completada');
        this.showRedirectButton = true;
      }
    });
  }

  redirectToHome(): void {
    this.router.navigate(['/']);
  }
}
