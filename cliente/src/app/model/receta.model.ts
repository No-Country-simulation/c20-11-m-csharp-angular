// import { Categoria } from './categoria.model'; // Importar Categoria si está en otro archivo

// export class Receta {
//   recetaID: number;
//   nombre: string;
//   descripcion: string;
//   imageUrl: string;
//   categorias: Categoria[];
//   isDeleted: boolean;
//   create_at: Date;

//   constructor(
//     recetaID: number,
//     nombre: string,
//     descripcion: string,
//     imageUrl: string,
//     categorias: Categoria[],
//     isDeleted: boolean,
//     create_at: Date
//   ) {
//     this.recetaID = recetaID;
//     this.nombre = nombre;
//     this.descripcion = descripcion;
//     this.imageUrl = imageUrl;
//     this.categorias = categorias;
//     this.isDeleted = isDeleted;
//     this.create_at = create_at;
//   }
// }


import { Categoria } from './categoria.model'; // Importar Categoria si está en otro archivo

export class Usuario {
  usuarioID: number;
  nombre: string;

  constructor(usuarioID: number, nombre: string) {
    this.usuarioID = usuarioID;
    this.nombre = nombre;
  }
}

export class Review {
  reviewID: number;
  usuario: Usuario;
  comentario: string;
  calificacion: number;
  isDeleted: boolean;
  create_at: Date;

  constructor(
    reviewID: number,
    usuario: Usuario,
    comentario: string,
    calificacion: number,
    isDeleted: boolean,
    create_at: Date
  ) {
    this.reviewID = reviewID;
    this.usuario = usuario;
    this.comentario = comentario;
    this.calificacion = calificacion;
    this.isDeleted = isDeleted;
    this.create_at = create_at;
  }
}

export class Ingrediente {
  ingredienteID: number;
  nombre: string;
  cantidad: string;

  constructor(ingredienteID: number, nombre: string, cantidad: string) {
    this.ingredienteID = ingredienteID;
    this.nombre = nombre;
    this.cantidad = cantidad;
  }
}

export class Receta {
  recetaID: number;
  nombre: string;
  descripcion: string;
  imageUrl: string;
  puntuacion: number;
  usuario: Usuario;
  reviews: Review[];
  categorias: Categoria[];
  ingredientes: Ingrediente[];
  tiempoCoccion: string;
  isDeleted: boolean;
  create_at: Date;

  constructor(
    recetaID: number,
    nombre: string,
    descripcion: string,
    imageUrl: string,
    puntuacion: number,
    usuario: Usuario,
    reviews: Review[],
    categorias: Categoria[],
    ingredientes: Ingrediente[],
    tiempoCoccion: string,
    isDeleted: boolean,
    create_at: Date
  ) {
    this.recetaID = recetaID;
    this.nombre = nombre;
    this.descripcion = descripcion;
    this.imageUrl = imageUrl;
    this.puntuacion = puntuacion;
    this.usuario = usuario;
    this.reviews = reviews;
    this.categorias = categorias;
    this.ingredientes = ingredientes;
    this.tiempoCoccion = tiempoCoccion;
    this.isDeleted = isDeleted;
    this.create_at = create_at;
  }
}

