import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
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

  constructor(private router:Router, 
    private userService:UserService,
    private authService:AuthService,
    private fb: FormBuilder) {
      this.profileForm = this.fb.group({
        userName: ['', Validators.required]
    });
  }

  ngOnInit() {
    try {
      console.log(this.userService.getUser())
      let user = this.userService.getUser();
      this.userName = new FormControl(user.userName, Validators.pattern("[a-zA-Z0-9]+"));

      this.profileForm.patchValue({
        userName: user.userName
      });
  } catch (error) {
    console.error(error);
  }
  }

  cancel() {
    this.router.navigate(['tasks']);
  }

  saveProfile(formValues) {
    this.authService.updateUser(formValues.userName)
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