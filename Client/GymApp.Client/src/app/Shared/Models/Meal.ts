import { MealFood } from './MealFood';

export type Meal = {
  id: number;
  name: string;
  calories: number;
  protein: number;
  carbohydrates: number;
  fats: number;
  mealFoods: MealFood[];
};
