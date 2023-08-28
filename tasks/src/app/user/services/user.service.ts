import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { environment } from "src/environments/environment";
import { IAuthorizedUser } from "../models/user.model";
import { IUser } from "src/app/tasks/models/user.model";
import { AuthService } from "./auth.service";

@Injectable()
  export class UserService {
    private currentUser: IUser;
    private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
      }

    setUser(user: IUser) {
      this.currentUser = user;

      let authorizedUser:IAuthorizedUser

      authorizedUser = {
        userName: user.userName
      }
    }
  
    getUser() {
      return this.currentUser;
    }

    async addUserAsync(userName: string, email: string, password: string): Promise<IUser> {
      const registerData = {
        userName: userName,
        email: email,
        password: password
      };

      try {
        const response = await this.http.post<IUser>(`${this.apiUrl}/users`, registerData).toPromise();
        console.log(response)
        return response;
      } catch (error) {
        this.handeError(error);
        return null;
      }
    }
    

    clearUser() {
      this.currentUser = null;
    }


    private handeError<T> (operation = 'operation', result?: T) {
      return (error: any) : Observable<T> => {
        console.error(error);
        return of(result as T);
      }
  }
}
