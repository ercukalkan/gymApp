import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { ExerciseService } from '../../Core/Services/exercise-service';
import { RouterLink } from '@angular/router';
import { ExerciseItemComponent } from './exercise-item-component/exercise-item-component';

@Component({
  selector: 'app-exercise-list-component',
  imports: [
    ExerciseItemComponent,
    RouterLink
  ],
  templateUrl: './exercise-list-component.html',
  styleUrl: './exercise-list-component.css',
})
export class ExerciseListComponent {
  exercises: any[] = [];
  exerciseService = inject(ExerciseService);
  cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.loadExercises();
  }

  loadExercises() {
    this.exerciseService.getExercises().subscribe({
      next: (data) => {
        this.exercises = data;
        this.cdr.detectChanges(); // Force change detection
      },
      error: (err) => console.error('Error loading exercises:', err)
    });
  }
}
