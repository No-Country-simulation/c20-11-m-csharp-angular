import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './componentes/header/header.component';
import { NavBarComponent } from './componentes/navBar/navBar.component';
import { BodyComponent } from './componentes/body/body.component';
import { CategoriaComponent } from './componentes/categoria/categoria.component';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HeaderComponent,
    NavBarComponent,
    BodyComponent,
    CategoriaComponent,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
