import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-busqueda',
  templateUrl: './busqueda.component.html',
  styleUrls: ['./busqueda.component.css']
})
export class BusquedaComponent {
  searchTerm: string = '';

  constructor(private router: Router) {}

  search(): void {
    if (this.searchTerm.trim()) {
      this.router.navigate(['/resultados'], { queryParams: { q: this.searchTerm } });
    }
  }
}
