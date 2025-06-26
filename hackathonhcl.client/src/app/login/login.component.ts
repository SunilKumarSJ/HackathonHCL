import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {


  admin = {
    username: '',
    password: ''
  };

  loginError: string | null = null;

  constructor(private router: Router) { }

  onLogin(): void {
    // Simple hardcoded check (replace with actual auth service)
    if (this.admin.username === 'admin' && this.admin.password === 'admin123') {
      this.loginError = null;
      this.router.navigate(['/dashboard']); // Navigate to admin dashboard
    } else {
      this.loginError = 'Invalid username or password';
    }
  }
}
