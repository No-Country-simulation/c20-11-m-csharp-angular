// auth.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isAuthenticated$: Observable<boolean> = this.isAuthenticatedSubject.asObservable();

  constructor() {
    // Verifica el estado de autenticación al inicializar el servicio
    this.checkAuthentication();
  }

  private checkAuthentication(): void {
    // Aquí puedes agregar la lógica para verificar si el usuario está autenticado
    const token = localStorage.getItem('authToken');
    this.isAuthenticatedSubject.next(!!token); // Asume que si hay un token, el usuario está autenticado
  }

  login(): void {
    // Tu lógica de autenticación
    localStorage.setItem('authToken', 'sampleToken'); // Ejemplo: guarda un token en localStorage
    this.isAuthenticatedSubject.next(true);
  }

  logout(): void {
    localStorage.removeItem('authToken');
    this.isAuthenticatedSubject.next(false);
  }
}
