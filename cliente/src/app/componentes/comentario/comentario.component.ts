import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

export interface Usuario {
  usuarioID: number;
  nombre: string;
}

export interface Review {
  reviewID: number;
  usuario: Usuario;
  comentario: string;
  calificacion: number;
  isDeleted: boolean;
  create_at: string;
}

@Component({
  selector: 'app-comentario',
  templateUrl: './comentario.component.html',
  styleUrl: './comentario.component.css',
  standalone:true,
  imports:[RouterOutlet, RouterLink, RouterLinkActive,CommonModule]
})
export class ComentarioComponent {



  all_comentarios:Array<Review> = []
  constructor(
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const recetaID = this.route.snapshot.paramMap.get('id');
    if (recetaID) {
      
    }
  }
  getAllComentarios(){

  } 

}
