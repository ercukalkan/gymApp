import { Injectable } from '@angular/core';
import { NutritionGenericService } from './nutrition-generic-service';
import { Meal } from '../../Shared/Models/Meal';

@Injectable({
  providedIn: 'root',
})
export class MealService extends NutritionGenericService<Meal> {
  constructor() {
    super('http://localhost:5000/api/nutrition', 'meal');
  }
}
