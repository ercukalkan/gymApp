import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { FoodService } from '../../../Core/Services/food-service';
import { FoodItemComponent } from './food-item-component/food-item-component';
import { RouterLink } from '@angular/router';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { PaginationParams } from '../../../Shared/Models/PaginationParams';
import { Pagination } from '../../../Shared/Models/Pagination';
import { Food } from '../../../Shared/Models/Food';

@Component({
  selector: 'app-food-list-component',
  imports: [FoodItemComponent, RouterLink, MatPaginatorModule],
  templateUrl: './food-list-component.html',
  styleUrl: './food-list-component.css',
})
export class FoodListComponent {
  data?: Pagination<Food>;
  foodService = inject(FoodService);
  cdr = inject(ChangeDetectorRef);

  paginationParams = new PaginationParams();

  ngOnInit(): void {
    this.loadFoods();
  }

  loadFoods() {
    this.foodService.getAll(this.paginationParams).subscribe({
      next: (data) => {
        this.data = data;
        this.cdr.detectChanges(); // Force change detection
      },
      error: (err) => console.error('Error loading foods:', err),
    });
  }

  handlePaginatorEvent(e: PageEvent) {
    this.paginationParams.pagenumber = e.pageIndex + 1;
    this.paginationParams.pagesize = e.pageSize;
    this.loadFoods();
  }
}
