import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FoodService } from '../../Core/Services/food-service';
import { FoodItemComponent } from './food-item-component/food-item-component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-food-list-component',
  imports: [
    FoodItemComponent,
    RouterLink
],
  templateUrl: './food-list-component.html',
  styleUrl: './food-list-component.css',
})
export class FoodListComponent {
  foods: any[] = [];
  foodService = inject(FoodService);
  cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.loadFoods();
  }

  loadFoods() {
    this.foodService.getFoods().subscribe({
      next: (data) => {
        this.foods = data;
        this.cdr.detectChanges(); // Force change detection
      },
      error: (err) => console.error('Error loading foods:', err)
    });
  }
}
