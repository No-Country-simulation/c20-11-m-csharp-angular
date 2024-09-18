import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
 import { API_ENDPOINT } from '../../../vars';

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {
  private apiUrl = `${API_ENDPOINT}/api/categorias/all?page=0&pageSize=100`;

  constructor(private http: HttpClient) {}

  getCategorias(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
