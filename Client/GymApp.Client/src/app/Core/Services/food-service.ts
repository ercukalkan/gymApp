import { Injectable } from '@angular/core';
import { Food } from '../../Shared/Models/Food';
import { NutritionGenericService } from './nutrition-generic-service';

@Injectable({
  providedIn: 'root',
})
export class FoodService extends NutritionGenericService<Food> {
  constructor() {
    super('http://localhost:5000/api/nutrition', 'food');
  }
}
