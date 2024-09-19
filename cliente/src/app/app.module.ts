import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './componentes/header/header.component';
import { NavBarComponent } from './componentes/navBar/navBar.component';
import { BodyComponent } from './componentes/body/body.component';
import { CategoriaComponent } from './componentes/categoria/categoria.component';
import { ChefsComponent } from './componentes/chefs/chefs.component';
import { VistaListasRecetasComponent } from './componentes/vista-listas-recetas/vista-listas-recetas.component';
import { BarraLateralComponent } from './componentes/barra-lateral/barra-lateral.component';
import { VistaProfileComponent } from './componentes/vista-profile/vista-profile.component';
import { VistaDetalleRecetaComponent } from './componentes/vista-detalle-receta/vista-detalle-receta.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HeaderComponent,
    NavBarComponent,
    BodyComponent,
    CategoriaComponent,
    ChefsComponent,
    VistaListasRecetasComponent,
    BarraLateralComponent,
    VistaProfileComponent,
    VistaDetalleRecetaComponent,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
