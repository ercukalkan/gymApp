import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent } from '@angular/material/dialog';
import { AuthUserCardComponent } from '../auth-user-card-component/auth-user-card-component';
import { MatListModule, MatSelectionList } from '@angular/material/list';

@Component({
  selector: 'app-role-picker-component',
  imports: [
    MatListModule,
    MatDialogContent,
    MatDialogContent,
],
  templateUrl: './role-picker-component.html',
  styleUrl: './role-picker-component.css',
})
export class RolePickerComponent {
  data = inject(MAT_DIALOG_DATA);
  selectedRoles: string[] = [];
  private dialogRef = inject(MatDialogRef<AuthUserCardComponent>);
  selectedRole: any;

  @ViewChild('roles') rolesList!: MatSelectionList;

  confirmRoles(selected: any[]) {
    const selectedRoles = selected.map(option => option.value);
    this.dialogRef.close(selectedRoles);
  }
}
