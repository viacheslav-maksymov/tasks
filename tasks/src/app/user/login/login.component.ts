import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
    templateUrl: './login.component.html',
    styles: [`
        em { float:right; color:#E05C65; padding-left:10px; }
    `]
})

export class LoginComponent implements OnInit{
    loginForm:FormGroup;
    private userName:FormControl
    private password:FormControl
    public isLoginError

    constructor(private authService:AuthService, private router:Router) {
    }

    ngOnInit() {
        this.userName = new FormControl('', Validators.pattern("[a-zA-Z0-9]+"));
        this.password = new FormControl('', Validators.pattern("[a-zA-Z0-9]+")) 
    
        this.loginForm = new FormGroup({
          userName: this.userName,
          password: this.password,
        })
      }

    login(formValues) {
        if (this.loginForm.valid) { 
        let result = this.authService.login(this.userName.value, this.password.value).then(
            result => {
                if (result)
                {
                    this.router.navigate(['tasks']);
                } else {
                    this.isLoginError = true;
                }
            } 
        )    
        }    
    }

    register() {
        this.router.navigate(['/user/register']);
    }
}