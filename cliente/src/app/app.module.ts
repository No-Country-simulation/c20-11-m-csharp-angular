import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
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
import { RedirectLoginComponent } from './componentes/redirect-login/redirect-login.component';
import { CrearRecetaComponent } from './componentes/create-receta/crear-receta.component';
import { LoginButtonComponent } from "./componentes/login-button/login-button.component";
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { VistaProfileComponent } from './componentes/vista-profile/vista-profile.component';
import { VistaListaRecetasComponent } from './componentes/vista-lista-recetas/vista-lista-recetas.component';
import { BusquedaComponent } from './componentes/busqueda/busqueda.component';
import { FormsModule } from '@angular/forms'; 


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
    CalificacionStarComponent,
    VistaProfileComponent,
    VistaListaRecetasComponent,
    BusquedaComponent,
  ],
  
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
        { path: '', component: HomeComponent },
        { path: 'recetasdeldia', component: RecetasDelDiaComponent },
        { path: 'chefs', component: ChefsComponent },
        { path: 'user', component: DashUserComponent },
        { path: 'categorias', component: CategoriasHomeComponent },
        { path: 'redirect', component: RedirectLoginComponent },
        { path: 'create', component: CrearRecetaComponent },
        { path: 'resultados', component: VistaListaRecetasComponent },
    ]),
    LoginButtonComponent,
    
],
  schemas:[CUSTOM_ELEMENTS_SCHEMA],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
