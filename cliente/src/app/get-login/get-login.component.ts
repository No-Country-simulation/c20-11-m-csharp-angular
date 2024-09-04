import { Component } from '@angular/core';
import { ApiLoginService } from '../Services/login/api-login.service';

@Component({
  selector: 'app-get-login',
  templateUrl: './get-login.component.html',
  styleUrl: './get-login.component.css'
})
export class GetLoginComponent {

  data: any;

  constructor(private apiService: ApiLoginService) { }

  fetchData(): void {
    this.apiService.getData().subscribe(response => {
      this.data = response;
      console.log(this.data);
    });
  }
}
