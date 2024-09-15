export class Categoria {
    categoriaID: number;
    nombre: string;
    imgUrl: string;
    totalRecetas: number;
  
    constructor(categoriaID: number, nombre: string, imgUrl: string, totalRecetas: number) {
      this.categoriaID = categoriaID;
      this.nombre = nombre;
      this.imgUrl = imgUrl;
      this.totalRecetas = totalRecetas;
    }
  }
  