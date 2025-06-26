import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  //
  loginFormGroup: FormGroup;
  submit = false;
  constructor(private formbuilder: FormBuilder, private userService: UserService, private router: Router) {
    this.loginFormGroup = this.formbuilder.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    })
  }

  getFormCOntrol(formControlName: string) {
    return this.loginFormGroup.get(formControlName)
  }

  login(): any {
    this.submit = true;
    if (this.loginFormGroup.valid && this.submit) {
      this.userService.login(this.loginFormGroup.value).subscribe((data: any) => {
        localStorage.removeItem('token');
        console.log(data.response);
        this.saveToken(data.response)
        this.router.navigate(['/app-users'])
        alert("Login successfully");
      })
    }
    else {
      alert("Form is not valid.");
      return false;
    }

  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  //removeToken() {
  //  this.userService.logout();
  //}
}
