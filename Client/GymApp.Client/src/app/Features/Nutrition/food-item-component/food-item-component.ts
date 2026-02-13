import { Component, Input, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FoodService } from '../../../Core/Services/food-service';
import { Food } from '../../../Shared/Models/Food';

@Component({
  selector: 'app-food-item-component',
  imports: [CommonModule, RouterLink],
  templateUrl: './food-item-component.html',
  styleUrl: './food-item-component.css',
})
export class FoodItemComponent {
  @Input() food?: Food;
  private foodService = inject(FoodService);

  onDelete(event: Event): void {
    event.stopPropagation(); // Prevent card navigation

    if (!confirm(`Delete "${this.food?.name}"? This cannot be undone.`)) {
      return;
    }

    if (this.food) {
      this.foodService.deleteFood(this.food.id).subscribe({
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
