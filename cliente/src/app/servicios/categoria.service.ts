import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RecetaService {

  private apiUrl = 'https://tu-api.com/recetas'; // Cambia esta URL por tu endpoint

  constructor(private http: HttpClient) { }

  obtenerRecetas(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}