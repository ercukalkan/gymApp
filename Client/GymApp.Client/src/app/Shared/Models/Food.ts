import { Meal } from './Meal';

export type Food = {
  id: number;
  name: string;
  calories: number;
  protein: number;
  carbohydrates: number;
  fats: number;
  mealFoods?: Meal[];
};
