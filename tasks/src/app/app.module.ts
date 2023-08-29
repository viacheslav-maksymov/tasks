import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router'

import { AppComponent } from './app.component';
import { NavBarComponent } from './navbar/navbar.component';
import { Error403Component } from './errors/403.component';
import { Error404Component } from './errors/404.component';

import { appRoutes } from './routes'
import { ToastrService } from './common/toastr.service';
import { AuthService } from './user/services/auth.service';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule, HttpHandler } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { AuthInterceptor } from './user/services/auth-interceptor.interceptor';
import { UserService } from './user/services/user.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    BrowserAnimationsModule
  ],
  declarations: [
    AppComponent,
    NavBarComponent,
    Error403Component,
    Error404Component,
  ],
  providers: [
    HttpClient,
    AuthService,
    UserService,
    ToastrService,
    CookieService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


