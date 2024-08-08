import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app.routes';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common'; 

import { LoginComponent } from './login/login.component';
import { CompanyComponent } from './company/company.component';

import { AuthService } from './services/auth.service';
import { CompanyService } from './services/company.service'; 
import { AuthInterceptor } from './auth-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CompanyComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [
    AuthService,
    CompanyService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
