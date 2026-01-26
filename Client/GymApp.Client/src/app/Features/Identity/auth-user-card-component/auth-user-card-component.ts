import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { AuthService } from '../../../Core/Services/auth-service';

@Component({
  selector: 'app-auth-user-card-component',
  imports: [
    CommonModule,
    MatDialogContent,
    ReactiveFormsModule
],
  templateUrl: './auth-user-card-component.html',
  styleUrl: './auth-user-card-component.css',
})
export class AuthUserCardComponent implements OnInit {
  data = inject(MAT_DIALOG_DATA);
  private authService = inject(AuthService);
  selectedRoles: string[] = [];
  private dialogRef = inject(MatDialogRef<AuthUserCardComponent>);
  userForm!: FormGroup;

  ngOnInit(): void {
    this.selectedRoles = [...this.data.roles];

    this.userForm = new FormGroup({
      email: new FormControl(this.data.email),
      username: new FormControl(this.data.username),
      firstName: new FormControl(this.data.firstName),
      lastName: new FormControl(this.data.lastName),
      phoneNumber: new FormControl(this.data.phoneNumber),
      dateOfBirth: new FormControl(this.data.dateOfBirth),
      roles: new FormControl(this.data.roles)
    });
  }

  onEdit() {
    let user = {
      UserDTO: {
        id: this.data.id,
        email: this.userForm.value.email,
        username: this.userForm.value.username,
        firstname: this.userForm.value.firstName,
        lastname: this.userForm.value.lastName,
        phoneNumber: this.userForm.value.phoneNumber,
        dateOfBirth: this.userForm.value.dateOfBirth,
      },
      Roles: this.userForm.value.roles
    };

    this.authService.updateUser(this.data.id, user).subscribe(
      () => this.dialogRef.close()
    );
  }
  
  deleteUser() {

  }

  onRoleClick(role: string) {
    this.selectedRoles = this.selectedRoles.filter(sr => sr !== role);
    this.userForm.patchValue({ roles: this.selectedRoles });
  }
}
