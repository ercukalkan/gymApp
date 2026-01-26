import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialog, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { AuthService } from '../../../Core/Services/auth-service';
import { RolePickerComponent } from '../role-picker-component/role-picker-component';

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
  private authUserDialogRef = inject(MatDialogRef<AuthUserCardComponent>);
  userForm!: FormGroup;
  allRoles: string[] = [];
  rolesWithSelections: any[] = [];
  private cdr = inject(ChangeDetectorRef);

  readonly dialog = inject(MatDialog);

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
      () => this.authUserDialogRef.close()
    );
  }
  
  deleteUser() {
    this.authService.deleteUser(this.data.id).subscribe(
      () => this.authUserDialogRef.close()
    );
  }

  openDialog() {
    this.authService.getRoles().subscribe(res => {
      this.allRoles = res;

      this.rolesWithSelections = this.createRolesWithSelectionsObject(this.selectedRoles, res);
      
      const dialogRef = this.dialog.open(RolePickerComponent, {
        height: '500px',
        width: '600px',
        data: this.rolesWithSelections
      });

      dialogRef.afterClosed().subscribe(res => {
        if (res) {
          this.selectedRoles = res;
          this.userForm.patchValue({ roles: res });
          this.cdr.detectChanges();
        }
      });
    });
  }

  private createRolesWithSelectionsObject(arrCurrentRoles: string[], arrAllRoles: string[]) {
    let resultArray: any[] = [];

    arrAllRoles.map(r => {
      resultArray.push({
        role: r,
        isSelected: arrCurrentRoles.includes(r)
      })
    });

    return resultArray;
  }
}
