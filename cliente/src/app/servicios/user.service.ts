import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../model/usuario.model'; 
import { API_ENDPOINT } from '../../../vars';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://your-api-url.com/api/usuarios'; // URL de tu API, ajusta esto

  constructor(private http: HttpClient) {}

// src/app/services/user.service.ts
    getUserById(usuarioID: number): Observable<Usuario> {
    const url = `${API_ENDPOINT}/api/usuario/${usuarioID}`; // Asegúrate de que la URL sea correcta
    return this.http.get<Usuario>(url);
  }
  
  
}
