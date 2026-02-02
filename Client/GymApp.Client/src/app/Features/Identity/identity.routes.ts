import { Routes } from "@angular/router";
import { LoginComponent } from "./login-component/login-component";
import { RegisterComponent } from "./register-component/register-component";
import { AuthManagementComponent } from "./auth-management-component/auth-management-component";
import { authGuard } from "../../Core/Guards/auth-guard";

export const identityRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'auth-management', component: AuthManagementComponent, canActivate: [authGuard] }
];