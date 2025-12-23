import { Routes } from '@angular/router';
import { FoodListComponent } from './Features/Nutrition/food-list-component';
import { FoodDetailComponent } from './Features/Nutrition/food-detail-component/food-detail-component';

export const routes: Routes = [
    { path: 'food', component: FoodListComponent },
    { path: 'food/:id', component: FoodDetailComponent },
];
