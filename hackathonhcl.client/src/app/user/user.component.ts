import { Component, OnInit } from '@angular/core';
import { user } from '../models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit
{
  users: user[]
  constructor(private userService: UserService) {
    //
  }

  ngOnInit() {
    this.userService.getAll().subscribe((data: any) => {
      this.users = data.response
      console.log(this.users);
    })
  }
}
