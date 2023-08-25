import { Component, OnInit } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { AuthService } from '../services/auth.service'
import { Router } from '@angular/router'
import { UserService } from '../services/user.service'

@Component({
  templateUrl: './profile.component.html',
  styles: [`
    em { float:right; color:#E05C65; padding-left:10px; }
    .error input { background-color:#E3C3C5 }
  `]
})
export class ProfileComponent implements OnInit {
  profileForm:FormGroup
  private userName:FormControl
  private email:FormControl

  constructor(private router:Router, 
    private userService:UserService,
    private authService:AuthService) {

  }

  ngOnInit() {
    this.userName = new FormControl(this.userService.getUser()?.userName, Validators.pattern("[a-zA-Z0-9]+"));
    this.email = new FormControl(this.userService.getUser()?.email) 

    this.profileForm = new FormGroup({
      userName: this.userName,
      email: this.email
    })
  }

  cancel() {
    this.router.navigate(['tasks']);
  }

  saveProfile(formValues) {
    this.authService.updateUser(formValues.email)
    this.router.navigate(['tasks']);
  }

  validateUserName() {
    return this.userName.valid
  }

  logout () {
    this.authService.logout()
    this.router.navigate(['/user/login']);
  }
}