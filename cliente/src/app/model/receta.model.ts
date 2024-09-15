import { Categoria } from './categoria.model'; // Importar Categoria si está en otro archivo

export class Receta {
  recetaID: number;
  nombre: string;
  descripcion: string;
  imageUrl: string;
  categorias: Categoria[];
  isDeleted: boolean;
  create_at: Date;

  constructor(
    recetaID: number,
    nombre: string,
    descripcion: string,
    imageUrl: string,
    categorias: Categoria[],
    isDeleted: boolean,
    create_at: Date
  ) {
    this.recetaID = recetaID;
    this.nombre = nombre;
    this.descripcion = descripcion;
    this.imageUrl = imageUrl;
    this.categorias = categorias;
    this.isDeleted = isDeleted;
    this.create_at = create_at;
  }
}



