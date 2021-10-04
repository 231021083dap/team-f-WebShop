import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service'

@Component({
  selector: 'app-newuser-page',
  templateUrl: './newuser-page.component.html',
  styleUrls: ['./newuser-page.component.css']
})
export class NewuserPageComponent implements OnInit {
  public newUser : User = {};


  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }

  // ---------------------- Create User ---------------------- -->
  create(user: User) : void {
    this.userService.create(user).subscribe(c => {this.newUser = c})
  }
  // ---------------------- Create User ---------------------- -->


}
