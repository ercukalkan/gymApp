import { Component, inject } from '@angular/core';
import { Router } from "@angular/router";
import { AuthService } from '../../../Core/Services/auth-service';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-login-component',
  imports: [FormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
})
export class LoginComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  email: string = '';
  password: string = '';
  isAuthenticated: boolean = false;

  login() {
    if (!this.email.trim()) {
      alert('Email is required.');
      return;
    }

    this.authService.login(this.email);
    this.router.navigate(['/']);
  }

  logout() {
    this.authService.logout();
    window.location.reload();
  }

}
