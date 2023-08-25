import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot  } from '@angular/router';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.checkLogin(route, state);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.checkLogin(route, state);
  }

  private checkLogin(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    console.log(state)
    if (state.url !== '/user/login' && state.url !== '/user/register' && !this.authService.isLoggedIn() ) {
      this.router.navigate(['/user/login']);
      return false;
    }

    if (this.authService.isLoggedIn() && (state.url === '/user/login' || state.url === '/user/register')) {
      this.router.navigate(['/user/profile']);
      return false;
    }

    return true;
  }
}