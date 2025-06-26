import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {
  addFormGroup: FormGroup;
  submit = false;
  constructor(private formbuilder: FormBuilder, private userService: UserService, private router: Router) {
    this.addFormGroup = this.formbuilder.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    })
  }


}
