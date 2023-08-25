import { Component } from "@angular/core"
import { AuthService } from "../user/services/auth.service";
import { UserService } from "../user/services/user.service";

@Component({
    selector: 'nav-bar',
    templateUrl: './navbar.component.html',
    styles: [`
        .nav.navbar-nav {font-size: 15px;}
        #searchForm {margin-right: 100px;}
        @media (max-width: 1200px) {#searchForm {display}}
        li > a.active { color: #F97924; }
    `]
})

export class NavBarComponent{
    constructor(public authService:AuthService, public userService: UserService) {
        console.log()
    }
}