// src/app/services/recetas.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_ENDPOINT } from '../../../vars';
import { map } from 'rxjs/operators';


export interface Usuario {
    usuarioID: number;
    nombre: string;
}
  
interface Receta {
    recetaID: number;
    usuarioID: number;
    nombre: string;
    descripcion: string;
    imageUrl: string;
    tiempoCoccion: string;
    isDeleted: boolean;
    create_at: string;
}
  
export interface Review {
    reviewID: number;
    usuario: Usuario;
    receta: Receta;
    comentario: string;
    calificacion: number;
    isDeleted: boolean;
    create_at: string;
}

export interface CrearReview {
    recetaID: number;
    comentario: string;
    calificacion: number;
}
  
  
@Injectable({
  providedIn: 'root'
})
export class ComentarioService {
  private getAllComentsUrl = `${API_ENDPOINT}/review/:id?RecetaId=`;
  private postReviewUrl = `${API_ENDPOINT}/review`;

    constructor(private http: HttpClient) {}
    
    getAllComentarios(id: string): Observable<Review[]> {
        return this.http.get<Review[]>(`${this.getAllComentsUrl}${id}`,{withCredentials:true});
    }
    
    AddReview(newReview:CrearReview){
        return this.http.post<Review>(`${this.postReviewUrl}`,newReview,{
            withCredentials:true,
        });
    }
  
}
