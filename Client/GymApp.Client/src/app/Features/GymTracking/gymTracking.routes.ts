import { Routes } from "@angular/router";
import { ExerciseListComponent } from "./exercise-list-component";
import { ExerciseDetailComponent } from "./exercise-detail-component/exercise-detail-component";

export const gymTrackingRoutes: Routes = [
    { path: '', redirectTo: 'exercise', pathMatch: 'full' },
    { path: 'exercise', component: ExerciseListComponent },
    { path: 'exercise/add', component: ExerciseDetailComponent },
    { path: 'exercise/:id', component: ExerciseDetailComponent },
];