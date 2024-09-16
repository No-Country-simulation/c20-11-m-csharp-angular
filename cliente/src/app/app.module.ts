import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './vistas/home/home.component';
import { ChefsComponent } from './vistas/chefs-home/chefs.component';
import { RecetasDelDiaComponent } from './componentes/recetasdeldia-home/recetasdeldia.component';
import { DashUserComponent } from './vistas/dash-user/dash-user.component';
import { RecetasComponent } from './vistas/recetas/recetas.component';
import { CalificacionStarComponent } from './componentes/calificacion-star/calificacion-star.component';
import { HttpClientModule } from '@angular/common/http';
import { NavBarComponent } from './componentes/nav-bar/nav-bar.component';
import { CategoriasComponent } from './componentes/categoria/categoria.component';
import { CategoriasHomeComponent } from './vistas/categorias-home/categorias-home.component';
import { HeaderComponent } from './componentes/header/header.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ChefsComponent,
    RecetasDelDiaComponent,
    DashUserComponent, 
    RecetasComponent,
    NavBarComponent,
    CategoriasComponent,
    CategoriasHomeComponent,
    HeaderComponent,
    CalificacionStarComponent
  ],
  
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, 
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'recetasdeldia', component: RecetasDelDiaComponent },
      { path: 'chefs', component: ChefsComponent },
      { path: 'user', component: DashUserComponent },
      { path: 'categorias', component: CategoriasHomeComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
