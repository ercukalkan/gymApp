import { Component, inject, Input } from '@angular/core';
import { Meal } from '../../../../Shared/Models/Meal';
import { MealService } from '../../../../Core/Services/meal-service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-meal-item-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './meal-item-component.html',
  styleUrl: './meal-item-component.css',
})
export class MealItemComponent {
  @Input() meal?: Meal;
  private mealService = inject(MealService);

  onDelete(event: Event): void {
    event.stopPropagation(); // Prevent card navigation

    if (!confirm(`Delete "${this.meal?.name}"? This cannot be undone.`)) {
      return;
    }

    if (this.meal) {
      this.mealService.delete(this.meal.id).subscribe({
        next: () => {
          // Optionally refresh parent or emit event
          window.location.reload(); // Simple refresh - can be improved with event emitter
        },
        error: (err) => {
          console.error('Error deleting food:', err);
          alert('Failed to delete food.');
        },
      });
    }
  }
}
