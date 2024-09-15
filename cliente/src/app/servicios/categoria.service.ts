import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {
  private apiUrl = 'https://tasty-api-staging.onrender.com/api/categorias/all?page=0&pageSize=100';

  constructor(private http: HttpClient) {}

  getCategorias(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
