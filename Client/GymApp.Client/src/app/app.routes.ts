import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'food', loadChildren: () => import('./Features/Nutrition/food.routes').then(m => m.foodRoutes) },
];
