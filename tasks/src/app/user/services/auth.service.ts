import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { UserService } from './user.service';
import { environment } from 'src/environments/environment';
import { IUser } from 'src/app/tasks/models/user.model';

@Injectable()
export class AuthService {
  currentUser:IUser
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient,
     private cookieService: CookieService,
     private userService: UserService) {
      this.authorizeAsync();
   }



  async login(username: string, password: string):Promise<boolean> {

    let credentials = {
      "userName": username,
      "password": password
    }

    console.log('LOGIN')

    try {
      const response = await this.http.post<any>(`${this.apiUrl}/authentication/authenticate`, credentials).toPromise();
      console.log('response:', response);
      this.saveToken(response.token);
      return await this.authorizeAsync();
    } catch (error) {
      this.handeError(error);
      if (error.status === 404) {
        return false;
      }
    }   
  }

  async authorizeAsync():Promise<boolean> {
    let response:IUser
    try {
      response = await this.http.get<IUser>(`${this.apiUrl}/users`).toPromise()
      console.log('authorized user: ' + response)
      this.userService.setUser(response)
      return true;
    }
    catch (error) {
      this.handeError(error);
    }      
  }

  private saveToken(token: string) {
    const expires = new Date();
    expires.setHours(expires.getHours() + 8); 

    this.cookieService.set('auth_token', token, {
      expires,
      sameSite: 'Strict'
    });
  }

  async registerAsync(userName:string, email:string, password:string ):Promise<boolean> {
    let user = await this.userService.addUserAsync(userName, email, password);

    if (!user)
      return false;

   return this.login(userName, password)
  }

  isLoggedIn(): boolean {
    if (!this.cookieService.get('auth_token')) {
      return false;
    }

    return this.userService.getUser() !== undefined
  }

  logout(): void {
    this.userService.clearUser();
    this.cookieService.delete('auth_token');
  }

  updateUser(email:string) {
    console.log(email)
  }

  private handeError<T> (operation = 'operation', result?: T) {
    return (error: any) : Observable<T> => {
      console.error(error);
      return of(result as T);
    }
  }
}
