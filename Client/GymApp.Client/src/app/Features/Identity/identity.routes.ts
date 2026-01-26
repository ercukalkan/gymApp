import { Routes } from "@angular/router";
import { LoginComponent } from "./login-component/login-component";
import { RegisterComponent } from "./register-component/register-component";
import { AuthManagementComponent } from "./auth-management-component/auth-management-component";
import { AuthUserCardComponent } from "./auth-user-card-component/auth-user-card-component";

export const identityRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'auth-management', component: AuthManagementComponent },
    { path: 'auth-user', component: AuthUserCardComponent }
];