import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";
import { UserService } from "../services/user.service";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
    templateUrl: './register.component.html',
    styles: [`
        em { float:right; color:#E05C65; padding-left:10px; }
        .error input { background-color:#E3C3C5 }
  `]
})

export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    private userName:FormControl
    private email:FormControl
    private password:FormControl
    private passwordConfirm:FormControl
    public isRegisterError

    constructor(private authService:AuthService,
         private router:Router,
          private userService:UserService,
          private fb: FormBuilder) {
    }

    ngOnInit() {
        this.userName = new FormControl('', Validators.pattern("[a-zA-Z0-9]+"));
        this.email = new FormControl('') 
        this.password = new FormControl('', Validators.pattern("[a-zA-Z0-9]+")) 
        this.passwordConfirm = new FormControl('', Validators.pattern("[a-zA-Z0-9]+")) 
    
        this.registerForm = new FormGroup({
          userName: this.userName,
          email: this.email,
          password: this.password,
          passwordConfirm: this.passwordConfirm
        })
      }

    register(formValues) {
        if (this.registerForm.valid) { 
            this.authService.registerAsync(formValues.userName, formValues.email, formValues.password).then(
                result => {
                    if (result)
                    {
                        this.router.navigate(['tasks']);
                    } else {
                        this.isRegisterError = true;
                    }
                }
            )
        }
    }

    cancel() {
        this.router.navigate(['/user/login']);
    }

    validatePasswords() {
        if (this.password.touched && this.passwordConfirm.touched) {
            return this.password.value === this.passwordConfirm.value
        }

        return true
    }
}