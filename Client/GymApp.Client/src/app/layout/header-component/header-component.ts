import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../../Core/Services/auth-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header-component',
  imports: [
    CommonModule,
    RouterLink,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    MatDividerModule,
  ],
  templateUrl: './header-component.html',
  styleUrl: './header-component.css',
})
export class HeaderComponent implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);

  isAuthenticated: boolean = false;
  fullName: string | null = null;
  isAdmin: boolean = false;

  ngOnInit() {
    this.authService.authState$.subscribe(state => {
      this.isAuthenticated = state.isAuthenticated;
      this.fullName = state.firstName + ' ' + state.lastName;
      this.isAdmin = state.isAdmin;
    });
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/']);
        window.location.reload();
      },
      error: err => console.log(err)
    } 
    );
  }
}
