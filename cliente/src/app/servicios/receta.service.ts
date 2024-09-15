// src/app/services/recetas.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Receta } from '../model/receta.model'; // Asegúrate de tener este modelo definido

@Injectable({
  providedIn: 'root'
})
export class RecetasService {
  private apiUrl = 'https://tasty-api-staging.onrender.com/api/receta';
  private apiUrlOrdenadas = 'https://tasty-api-staging.onrender.com/api/receta/order';

  constructor(private http: HttpClient) {}

  // Método para obtener todas las recetas
  getRecetas(): Observable<Receta[]> {
    return this.http.get<Receta[]>(this.apiUrl);
  }

  // Método para obtener recetas ordenadas por calificación
  getRecetasOrdenadas(page: number = 0, pageSize: number = 10): Observable<Receta[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<Receta[]>(this.apiUrlOrdenadas, { params });
  }
}
