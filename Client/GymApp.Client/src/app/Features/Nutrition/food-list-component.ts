import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FoodService } from '../../Core/Services/food-service';
import { FoodItemComponent } from './food-item-component/food-item-component';
import { RouterLink } from '@angular/router';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-food-list-component',
  imports: [FoodItemComponent, RouterLink, MatPaginatorModule],
  templateUrl: './food-list-component.html',
  styleUrl: './food-list-component.css',
})
export class FoodListComponent {
  data: any;
  foodService = inject(FoodService);
  cdr = inject(ChangeDetectorRef);

  pagingParams = {
    pageNumber: 1,
    pageSize: 4,
  };

  ngOnInit(): void {
    this.loadFoods();
  }

  loadFoods() {
    this.foodService.getFoods(this.pagingParams).subscribe({
      next: (data) => {
        this.data = data;
        this.cdr.detectChanges(); // Force change detection
      },
      error: (err) => console.error('Error loading foods:', err),
    });
  }

  handlePaginatorEvent(e: PageEvent) {
    this.pagingParams.pageNumber = e.pageIndex + 1;
    this.pagingParams.pageSize = e.pageSize;
    this.loadFoods();
  }
}
