import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import * as auth0 from 'auth0-js';

import {
  DOMAIN,
  AUDIENCE,
  SCOPE,
  CLIENT_ID,
  RESPONSE_TYPE,
  REDIRECT_URI,
} from "../../../../vars";

@Component({
  selector: 'app-login-button',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.css']
})
export class LoginButtonComponent {
  title = 'login';

  // Función que será llamada al hacer clic en el botón
  handleClick(): void {
    localStorage.clear();
    const webAuth = new auth0.WebAuth({
        domain:`${DOMAIN}`,
        clientID:`${CLIENT_ID}`
    })

    let url = webAuth.client.buildAuthorizeUrl({
        clientID: `${CLIENT_ID}`,
        responseType: `${RESPONSE_TYPE}`,
        redirectUri: `${REDIRECT_URI}`,
        scope: `${SCOPE}`,
        audience: `${AUDIENCE}`,
      });
    window.location.replace(url);
  }
}
