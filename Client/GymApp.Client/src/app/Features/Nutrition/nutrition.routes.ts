import { Routes } from '@angular/router';
import { FoodListComponent } from './Food/food-list-component';
import { FoodDetailComponent } from './Food/food-detail-component/food-detail-component';
import { MealListComponent } from './Meal/meal-list-component';
import { MealDetailComponent } from './Meal/meal-detail-component/meal-detail-component';

export const nutritionRoutes: Routes = [
  { path: '', redirectTo: 'food', pathMatch: 'full' },
  { path: 'food', component: FoodListComponent },
  { path: 'food/add', component: FoodDetailComponent },
  { path: 'food/:id', component: FoodDetailComponent },
  { path: 'meal', component: MealListComponent },
  { path: 'meal/add', component: MealDetailComponent },
  { path: 'meal/:id', component: MealDetailComponent },
];
