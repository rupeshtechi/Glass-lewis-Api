import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  isAuthenticated: boolean = false;

  constructor(private authService: AuthService, private router: Router) {
    this.authService.isAuthenticated$.subscribe(isAuthenticated => {
      this.isAuthenticated = isAuthenticated;
    });

  }

  title = 'GlassLewis.Company.UI';

  logout() : void  {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
