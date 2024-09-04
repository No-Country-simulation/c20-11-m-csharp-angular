import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GetLoginComponent } from './get-login/get-login.component';
import { ApiLoginService } from './Services/login/api-login.service';

@NgModule({
  declarations: [
    AppComponent,
    GetLoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [ApiLoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
