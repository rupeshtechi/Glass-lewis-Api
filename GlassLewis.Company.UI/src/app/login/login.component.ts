import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMsg: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    this.errorMsg = '';
    this.authService.login(this.username, this.password).subscribe({
      next: (result) => {
        if (result && result.status == 0) {
          this.router.navigate(['/company']);
        } else {
         this.errorMsg  = result.message;
        }
      },
      error: (ex) => {
        this.errorMsg = ex.error?.title;
      }
    });
  }
}
