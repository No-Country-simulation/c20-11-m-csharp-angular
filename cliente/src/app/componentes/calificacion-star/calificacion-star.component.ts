import { Component, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-calificacion-star',
  templateUrl: './calificacion-star.component.html',
  styleUrl: './calificacion-star.component.css'
})
export class CalificacionStarComponent implements OnChanges {
  rating: number = 4;
  starwidth:number=0;

  ngOnChanges(changes: SimpleChanges): void {

    this.starwidth = this.rating * 76/5;
  }
  
}