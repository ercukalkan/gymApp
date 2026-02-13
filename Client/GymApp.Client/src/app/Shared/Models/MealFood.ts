import { Food } from './Food';
import { Meal } from './Meal';

export type MealFood = {
  id: number;
  food: Food;
  meal: Meal;
};
