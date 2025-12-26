import { Routes } from "@angular/router";
import { FoodListComponent } from "./food-list-component";
import { FoodDetailComponent } from "./food-detail-component/food-detail-component";

export const nutritionRoutes: Routes = [
    { path: '', redirectTo: 'food', pathMatch: 'full' },
    { path: 'food', component: FoodListComponent },
    { path: 'food/add', component: FoodDetailComponent },
    { path: 'food/:id', component: FoodDetailComponent },
];