import { Routes } from "@angular/router";
import { FoodListComponent } from "./food-list-component";
import { FoodDetailComponent } from "./food-detail-component/food-detail-component";

export const foodRoutes: Routes = [
    { path: '', component: FoodListComponent },
    { path: 'add', component: FoodDetailComponent },
    { path: ':id', component: FoodDetailComponent },
];