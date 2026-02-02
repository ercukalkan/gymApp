import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FoodService } from '../../../Core/Services/food-service';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-food-detail-component',
  imports: [ReactiveFormsModule],
  templateUrl: './food-detail-component.html',
  styleUrl: './food-detail-component.css',
})
export class FoodDetailComponent implements OnInit {
  private foodService = inject(FoodService);
  private route = inject(ActivatedRoute);
  food?: any;
  cdr = inject(ChangeDetectorRef);
  mode: 'add' | 'edit' = 'edit';
  private router = inject(Router);
  isDeleting = false;

  // Form group for food details
  foodForm = new FormGroup({
    // Form controls for editing food details
    nameCtrl: new FormControl(this.food?.name || ''),
    caloriesCtrl: new FormControl(this.food?.calories || 0),
    proteinCtrl: new FormControl(this.food?.protein || 0),
    carbsCtrl: new FormControl(this.food?.carbohydrates || 0),
    fatsCtrl: new FormControl(this.food?.fats || 0),
  });

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.mode = id ? 'edit' : 'add';

    if (this.mode === 'edit') {
      this.loadFoodDetails(id);
    } else {
      // Initialize form for adding new food
      this.foodForm.reset({
        nameCtrl: '',
        caloriesCtrl: 0,
        proteinCtrl: 0,
        carbsCtrl: 0,
        fatsCtrl: 0,
      });
    }
  }

  onSubmit() {
    let food = {
      id: this.food?.id,
      name: this.foodForm.value.nameCtrl,
      calories: this.foodForm.value.caloriesCtrl,
      protein: this.foodForm.value.proteinCtrl,
      carbohydrates: this.foodForm.value.carbsCtrl,
      fats: this.foodForm.value.fatsCtrl,
    };

    if (this.mode === 'add') {
      this.createFood(food);
    } else if (this.mode === 'edit' && this.food) {
      this.updateFood(this.food.id, food);
    }
  }

  // Public method to be called from template
  onDelete(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.deleteFood(id);
    }
  }

  private createFood(food: any) {
    this.foodService.addFood(food).subscribe((response) => {
      this.routerRedirect();
    });
  }

  private updateFood(id: any, food: any) {
    this.foodService.updateFood(id, food).subscribe((response) => {
      this.routerRedirect();
    });
  }

  private deleteFood(id: any) {
    // Confirmation dialog
    if (
      !confirm(
        `Are you sure you want to delete "${this.food?.name}"? This action cannot be undone.`,
      )
    ) {
      return;
    }

    this.isDeleting = true;
    this.foodService.deleteFood(id).subscribe({
      next: () => {
        this.isDeleting = false;
        this.routerRedirect();
      },
      error: (err) => {
        this.isDeleting = false;
        console.error('Error deleting food:', err);
        alert('Failed to delete food. Please try again.');
      },
    });
  }

  private loadFoodDetails(id: any) {
    this.foodService.getFoodById(id).subscribe((data) => {
      this.food = data;
      this.foodForm.patchValue({
        nameCtrl: data.name || '',
        caloriesCtrl: data.calories || 0,
        proteinCtrl: data.protein || 0,
        carbsCtrl: data.carbohydrates || 0,
        fatsCtrl: data.fats || 0,
      });
      this.cdr.detectChanges();
    });
  }

  private routerRedirect() {
    this.router.navigate(['/nutrition/food']);
  }
}
