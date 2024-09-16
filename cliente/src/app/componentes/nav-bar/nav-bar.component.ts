import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Services/Login/AuthService';  // Ajusta la ruta según corresponda
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  isAuthenticated = false;

  ngOnInit(): void {
    this.isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';
  }

  logout(): void {
    localStorage.removeItem('isAuthenticated'); 
    localStorage.removeItem('nombre'); 
    this.isAuthenticated = false;
  }
}
