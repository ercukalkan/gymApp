import { Component, inject, Input } from '@angular/core';
import { ExerciseService } from '../../../Core/Services/exercise-service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-exercise-item-component',
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './exercise-item-component.html',
  styleUrl: './exercise-item-component.css',
})
export class ExerciseItemComponent {
  @Input() exercise: any;
  private exerciseService = inject(ExerciseService);

  onDelete(event: Event): void {
    event.stopPropagation();
    if (!confirm(`Delete "${this.exercise.name}"? This cannot be undone.`)) {
      return;
    }

    this.exerciseService.deleteExercise(this.exercise.id).subscribe({
      next: () => {
        // Optionally refresh parent or emit event
        window.location.reload();  // Simple refresh - can be improved with event emitter
      },
      error: (err) => {
        console.error('Error deleting exercise:', err);
        alert('Failed to delete exercise.');
      }
    });
  }

  onEdit(event: Event): void {
    event.stopPropagation();
    // Navigation handled by RouterLink in template
  }

  onToggleComplete(event: Event): void {
    event.stopPropagation();
    // Toggle completion status will be implemented here
  }
}
