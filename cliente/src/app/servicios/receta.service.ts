// src/app/services/recetas.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Receta } from '../model/receta.model';
import { API_ENDPOINT } from '../../../vars';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class RecetasService {
  private apiUrl = `${API_ENDPOINT}/api/receta`;
  private apiUrlOrdenadas = `${API_ENDPOINT}/api/receta/order`;
  private apiUrlPorCategoria = `${API_ENDPOINT}/api/categorias`;

  constructor(private http: HttpClient) {}

  getRecetas(): Observable<Receta[]> {
    return this.http.get<Receta[]>(this.apiUrl);
  }

  getRecetasOrdenadas(page: number = 0, pageSize: number = 10): Observable<Receta[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<Receta[]>(this.apiUrlOrdenadas, { params });
  }

 getRecetasPorCategoria(categoriaId: number): Observable<Receta[]> {
  const url = `${this.apiUrlPorCategoria}/${categoriaId}`;
  return this.http.get<{ recetas: Receta[] }>(url).pipe(
    map(response => response.recetas)
  );
}

  
}
