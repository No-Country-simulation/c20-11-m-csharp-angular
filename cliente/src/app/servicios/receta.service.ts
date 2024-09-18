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
<<<<<<< HEAD
=======

>>>>>>> bfcce0f171342bfc12c78ba9770da42775127adf

@Injectable({
  providedIn: 'root'
})
export class RecetasService {
  private apiUrl = `${API_ENDPOINT}/api/receta`;
<<<<<<< HEAD
  private apiUrlOrdenadas = "https://tasty-api-staging.onrender.com/api/receta/order";
  private apiUrlPorCategoria = "https://tasty-api-staging.onrender.com/api/categorias";
  private apiUrlBusqueda = "https://tasty-api-staging.onrender.com/api/receta"; // Endpoint para búsqueda
=======
  private apiUrlOrdenadas = `${API_ENDPOINT}/api/receta/order`;
  private apiUrlPorCategoria = `${API_ENDPOINT}/api/categorias`;
>>>>>>> bfcce0f171342bfc12c78ba9770da42775127adf

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

<<<<<<< HEAD
  // Método para obtener recetas por categoría
  getRecetasPorCategoria(categoriaId: number): Observable<Receta[]> {
    const url = `${this.apiUrlPorCategoria}/${categoriaId}`;
    return this.http.get<{ recetas: Receta[] }>(url).pipe(
      map(response => response.recetas)
    );
  }

  // Método para buscar recetas
  buscarRecetas(query: string): Observable<Receta[]> {
    const params = new HttpParams().set('S', query);
    return this.http.get<Receta[]>(this.apiUrlBusqueda, { params });
  }
=======
 getRecetasPorCategoria(categoriaId: number): Observable<Receta[]> {
  const url = `${this.apiUrlPorCategoria}/${categoriaId}`;
  return this.http.get<{ recetas: Receta[] }>(url).pipe(
    map(response => response.recetas)
  );
}
>>>>>>> bfcce0f171342bfc12c78ba9770da42775127adf

  getRecetasPorBusqueda(query: string): Observable<Receta[]> {
    const url = `https://tasty-api-staging.onrender.com/api/receta?S=${query}`;
    return this.http.get<Receta[]>(url);
  }
  
}
