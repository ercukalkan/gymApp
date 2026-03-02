import { Food } from './Food';

export type Meal = {
  id: number;
  name: string;
  calories: number;
  protein: number;
  carbohydrates: number;
  fats: number;
  mealFoods: Food[];
};
