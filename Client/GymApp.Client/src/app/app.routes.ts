import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'nutrition', loadChildren: () => import('./Features/Nutrition/nutrition.routes').then(m => m.nutritionRoutes) },
    { path: 'gym-tracking', loadChildren: () => import('./Features/GymTracking/gymTracking.routes').then(m => m.gymTrackingRoutes) },
];
