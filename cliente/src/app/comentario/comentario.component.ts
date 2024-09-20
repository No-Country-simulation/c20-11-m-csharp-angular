import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ComentarioService, CrearReview, Review } from '../servicios/comentario.service';
import { FormsModule } from '@angular/forms';

export interface Usuario {
  usuarioID: number;
  nombre: string;
}

@Component({
  selector: 'app-comentario',
  templateUrl: './comentario.component.html',
  styleUrl: './comentario.component.css',
  standalone:true,
  imports:[RouterOutlet, RouterLink, RouterLinkActive,CommonModule,FormsModule]
})
export class ComentarioComponent {

  all_comentarios:Array<Review> = [];
  comentario: string = '';

  constructor(
    private route: ActivatedRoute,
    private comentarioService: ComentarioService
  ) {}

  async ngOnInit() {
    const recetaID = this.route.snapshot.paramMap.get('id');
    if (recetaID) {
      this.comentarioService.getAllComentarios(recetaID).subscribe({
        next: (data:any) => {
          this.all_comentarios = data;
          console.log(data);
        },
        error: (error:any) => {
          console.error('Error fetching comments', error);
        }
      });
    }
  }
    AddComentario() {
      const recetaID: string | null = this.route.snapshot.paramMap.get('id');

      if (recetaID) {
          const nuevoComentario: CrearReview = {
              recetaID: Number(recetaID),
              comentario: this.comentario,
              calificacion: 2
          };

          this.comentarioService.AddReview(nuevoComentario).subscribe({
              next: (response) => {
                  console.log('Comentario enviado con éxito', response);
                  if (response && response.reviewID) {
                      this.all_comentarios.push(response);
                      this.comentario = '';
                  }
              },
              error: (error) => {
                  console.error('Error al enviar el comentario', error);
              }
          });
      }
  }


}
