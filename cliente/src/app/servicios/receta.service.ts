// // src/app/services/recetas.service.ts
// import { Injectable } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { Receta } from '../model/receta.model';
// import { API_ENDPOINT } from '../../../vars';
// import { map } from 'rxjs/operators';

// @Injectable({
//   providedIn: 'root'
// })
// export class RecetasService {
//   private apiUrl = `${API_ENDPOINT}/api/receta`;
//   private apiUrlBusqueda = `${API_ENDPOINT}/api/receta`;
//   private apiUrlOrdenadas = `${API_ENDPOINT}/api/receta/order`;
//   private apiUrlPorCategoria = `${API_ENDPOINT}/api/categorias`;

//   constructor(private http: HttpClient) {}

//   getRecetas(): Observable<Receta[]> {
//     return this.http.get<Receta[]>(this.apiUrl);
//   }

//   getRecetasOrdenadas(page: number = 0, pageSize: number = 10): Observable<Receta[]> {
//     const params = new HttpParams()
//       .set('page', page.toString())
//       .set('pageSize', pageSize.toString());

//     return this.http.get<Receta[]>(this.apiUrlOrdenadas, { params });
//   }

//   // Método para obtener recetas por categoría
//   getRecetasPorCategoria(categoriaId: number): Observable<Receta[]> {
//     const url = `${this.apiUrlPorCategoria}/${categoriaId}`;
//     return this.http.get<{ recetas: Receta[] }>(url).pipe(
//       map(response => response.recetas)
//     );
//   }

//   // Método para buscar recetas
//   buscarRecetas(query: string): Observable<Receta[]> {
//     const params = new HttpParams().set('S', query);
//     return this.http.get<Receta[]>(this.apiUrlBusqueda, { params });
//   }

//   getRecetasPorBusqueda(query: string): Observable<Receta[]> {
//     const url = `${API_ENDPOINT}/api/receta?S=${query}`;
//     return this.http.get<Receta[]>(url);
//   }

//   getRecetaById(id: string): Observable<Receta> {
//     const url = `${API_ENDPOINT}/api/receta/:id?ID=${id}`;
//     console.log(`Fetching receta with URL: ${url}`);
//     return this.http.get<Receta>(url);
//   }

//   // Método para obtener las recetas creadas por un usuario
//   getRecetasByUserId(usuarioID: number): Observable<Receta[]> {
//     const url = `${API_ENDPOINT}/api/receta/user/${usuarioID}`;
//     return this.http.get<Receta[]>(url);
//   }
  

  
  
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

  buscarRecetas(query: string): Observable<Receta[]> {
    const params = new HttpParams().set('S', query);
    return this.http.get<Receta[]>(this.apiUrlBusqueda, { params });
  }

  getRecetasPorBusqueda(query: string): Observable<Receta[]> {
    const url = `${API_ENDPOINT}/api/receta?S=${query}`;
    return this.http.get<Receta[]>(url);
  }

  getRecetaById(id: string): Observable<Receta> {
    const url = `${API_ENDPOINT}/api/receta/:id?ID=${id}`;
    console.log(`Fetching receta with URL: ${url}`);
    return this.http.get<Receta>(url);
  }

  getRecetasByUserId(usuarioID: number): Observable<Receta[]> {
    const url = `${API_ENDPOINT}/api/receta/usuarios/${usuarioID}`; // Asegúrate que esta URL sea la correcta
    return this.http.get<Receta[]>(url);
  
}

}
