import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Pagination } from '../../../Shared/Models/Pagination';
import { Meal } from '../../../Shared/Models/Meal';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { PaginationParams } from '../../../Shared/Models/PaginationParams';
import { MealService } from '../../../Core/Services/meal-service';
import { RouterLink } from '@angular/router';
import { MealItemComponent } from './meal-item-component/meal-item-component';

@Component({
  selector: 'app-meal-list-component',
  imports: [RouterLink, MatPaginatorModule, MealItemComponent],
  templateUrl: './meal-list-component.html',
  styleUrl: './meal-list-component.css',
})
export class MealListComponent {
  data?: Pagination<Meal>;
  mealService = inject(MealService);
  cdr = inject(ChangeDetectorRef);

  paginationParams = new PaginationParams();

  ngOnInit(): void {
    this.loadMeals();
  }

  loadMeals() {
    this.mealService.getAll(this.paginationParams).subscribe({
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
    this.loadMeals();
  }
}
