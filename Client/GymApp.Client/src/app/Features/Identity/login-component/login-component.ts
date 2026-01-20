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

  login() {
    const data = {
      email: this.email,
      password: this.password
    };

    this.authService.login(data).subscribe(() =>
      {
        this.router.navigate(['/']);
      }
    );
  }
}
