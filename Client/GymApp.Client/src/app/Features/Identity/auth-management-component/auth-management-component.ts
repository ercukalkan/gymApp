import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../Core/Services/auth-service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AuthUserCardComponent } from '../auth-user-card-component/auth-user-card-component';

@Component({
  selector: 'app-auth-management-component',
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatDialogModule
],
  templateUrl: './auth-management-component.html',
  styleUrl: './auth-management-component.css',
})
export class AuthManagementComponent implements OnInit {
  private authService = inject(AuthService);
  private cdr = inject(ChangeDetectorRef);
  users: any = [];
  displayedColumns: string[] = ['username', 'email', 'firstName', 'lastName', 'dateOfBirth', 'phoneNumber'];
  readonly dialog = inject(MatDialog);
  selectedUser: any = null;

  ngOnInit(): void {
    this.loadUsers();
  }

  openDialog(row: any) {
    this.authService.getUser(row.id).subscribe(res => {
      this.selectedUser = res;
      const dialogRef = this.dialog.open(AuthUserCardComponent, {
        height: '900px',
        width: '600px',
        data: this.selectedUser
      });

      dialogRef.afterClosed().subscribe(() => {
        window.location.reload();
        this.loadUsers();
      })
    });
  }

  private loadUsers() {
    this.authService.getUsers().subscribe(
      res => {
        this.users = res;
        this.cdr.detectChanges();
      }
    );
  }
}
