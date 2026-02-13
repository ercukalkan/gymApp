import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Meal } from '../../../../Shared/Models/Meal';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Food } from '../../../../Shared/Models/Food';
import {
  MatFormField,
  MatLabel,
  MatOption,
  MatSelect,
  MatSelectTrigger,
} from '@angular/material/select';
import { MealService } from '../../../../Core/Services/meal-service';

@Component({
  selector: 'app-meal-details-component',
  imports: [ReactiveFormsModule, MatFormField, MatSelect, MatLabel, MatOption],
  templateUrl: './meal-detail-component.html',
  styleUrl: './meal-detail-component.css',
})
export class MealDetailComponent {
  private mealService = inject(MealService);
  private route = inject(ActivatedRoute);
  meal?: Meal;
  cdr = inject(ChangeDetectorRef);
  mode: 'add' | 'edit' = 'edit';
  private router = inject(Router);
  isDeleting = false;

  toppingList: string[] = ['Extra cheese', 'Mushroom', 'Onion', 'Pepperoni', 'Sausage', 'Tomato'];

  // Form group for food details
  foodForm = new FormGroup({
    // Form controls for editing food details
    nameCtrl: new FormControl(this.meal?.name || ''),
    caloriesCtrl: new FormControl(this.meal?.calories || 0),
    proteinCtrl: new FormControl(this.meal?.protein || 0),
    carbsCtrl: new FormControl(this.meal?.carbohydrates || 0),
    fatsCtrl: new FormControl(this.meal?.fats || 0),
    mealFoodsCtrl: new FormControl(this.meal?.mealFoods || []),
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
    let meal: Meal = {
      id: this.meal?.id!,
      name: this.foodForm.value.nameCtrl!,
      calories: this.foodForm.value.caloriesCtrl!,
      protein: this.foodForm.value.proteinCtrl!,
      carbohydrates: this.foodForm.value.carbsCtrl!,
      fats: this.foodForm.value.fatsCtrl!,
      mealFoods: this.foodForm.value.mealFoodsCtrl!,
    };

    if (this.mode === 'add') {
      this.createFood(meal);
    } else if (this.mode === 'edit' && this.meal) {
      -this.updateFood(this.meal.id, meal);
    }
  }

  // Public method to be called from template
  onDelete(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.deleteFood(id);
    }
  }

  private createFood(meal: Meal) {
    this.mealService.add(meal).subscribe((response) => {
      this.routerRedirect();
    });
  }

  private updateFood(id: number, meal: Meal) {
    this.mealService.update(id, meal).subscribe((response) => {
      this.routerRedirect();
    });
  }

  private deleteFood(id: number) {
    // Confirmation dialog
    if (
      !confirm(
        `Are you sure you want to delete "${this.meal?.name}"? This action cannot be undone.`,
      )
    ) {
      return;
    }

    this.isDeleting = true;
    this.mealService.delete(id).subscribe({
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

  private loadFoodDetails(id: number) {
    this.mealService.getById(id).subscribe((data) => {
      this.meal = data;
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
