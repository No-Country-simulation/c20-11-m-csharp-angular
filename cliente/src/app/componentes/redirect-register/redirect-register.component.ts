import { Component, OnInit } from '@angular/core';
import { API_ENDPOINT } from '../../../../vars';
import { HttpClient } from '@angular/common/http';
import { User } from '../redirect-login/redirect-login.component';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-redirect-register',
  templateUrl: './redirect-register.component.html',
  styleUrl: './redirect-register.component.css',
  standalone:true,
  imports:[RouterOutlet, RouterLink, RouterLinkActive,CommonModule],
})
export class RedirectRegisterComponent implements OnInit {
  title = 'register';
  user: User | null = null;
  isLoading: boolean = true;
  code: string | null = null;
  showRedirectButton: boolean = false;
  showIsRegistered: boolean = false;

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
      },
      error: (e) => {
        console.error('Error al obtener los datos de la API:', e);
        this.showRedirectButton = true;
        this.showIsRegistered = true;
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
