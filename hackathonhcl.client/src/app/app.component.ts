import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserService } from './services/user.service';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  constructor(private http: HttpClient, private userService: UserService) { }

  ngOnInit()
  {
    this.removeToken();
  }
  isLoggedIn() {
    return this.userService.isLoggedIn();
  }

  isInRoles(roles: string[]) {
    return this.userService.isInRole(roles);
  }

  removeToken() {
    localStorage.removeItem('token');
  }

  title = 'hackathonhcl.client';


}
