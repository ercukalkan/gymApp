import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { AuthService } from '../../../Core/Services/auth-service';

@Component({
  selector: 'app-register-component',
  imports: [RouterLink, FormsModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
})
export class RegisterComponent {
  private authService = inject(AuthService);

  firstName: string = '';
  lastName: string = '';
  username: string = '';
  birthDate: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      console.error('Passwords do not match');
      return;
    }

    const registerData = {
      FirstName: this.firstName,
      LastName: this.lastName,
      UserName: this.username,
      DateOfBirth: this.birthDate,
      Email: this.email,
      Password: this.password,
      Gender: 'Male'
    };

    this.authService.register(registerData).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        window.location.replace('/identity/login');
      },
      error: (error) => {
        console.error('Registration failed', error);
      }
    });
  }
}
