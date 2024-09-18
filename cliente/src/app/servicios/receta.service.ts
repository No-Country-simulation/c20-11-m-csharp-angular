// // src/app/services/recetas.service.ts
// import { Injectable } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { Receta } from '../model/receta.model'; // Asegúrate de tener este modelo definido
// import { API_ENDPOINT } from '../../../vars';
// import { map } from 'rxjs/operators'; // Asegúrate de importar map


// @Injectable({
//   providedIn: 'root'
// })
// export class RecetasService {
//   private apiUrl = `${API_ENDPOINT}/api/receta`;
//   private apiUrlOrdenadas = "https://tasty-api-staging.onrender.com/api/receta/order";
//   private apiUrlPorCategoria = "https://tasty-api-staging.onrender.com/api/categorias"; // Nuevo endpoint para filtrar por categoría

//   constructor(private http: HttpClient) {}

//   // Método para obtener todas las recetas
//   getRecetas(): Observable<Receta[]> {
//     return this.http.get<Receta[]>(this.apiUrl);
//   }

//   // Método para obtener recetas ordenadas por calificación
//   getRecetasOrdenadas(page: number = 0, pageSize: number = 10): Observable<Receta[]> {
//     const params = new HttpParams()
//       .set('page', page.toString())
//       .set('pageSize', pageSize.toString());

//     return this.http.get<Receta[]>(this.apiUrlOrdenadas, { params });
//   }

//  // Método para obtener recetas por categoría
//  getRecetasPorCategoria(categoriaId: number): Observable<Receta[]> {
//   const url = `${this.apiUrlPorCategoria}/${categoriaId}`;
//   return this.http.get<{ recetas: Receta[] }>(url).pipe(
//     map(response => response.recetas) // Extraemos el array de recetas de la respuesta
//   );
// }

  
// }


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
  private apiUrlBusqueda = `${API_ENDPOINT}/api/receta`;
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

  getRecetasPorBusqueda(query: string): Observable<Receta[]> {
    const url = `${API_ENDPOINT}/api/receta?S=${query}`;
    return this.http.get<Receta[]>(url);
  }
  
}
