import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5181/api';

  public getFoods() {
    return this.http.get<any>(`${this.baseUrl}/food`);
  }

  public getFoodById(id: number) {
    return this.http.get<any>(`${this.baseUrl}/food/${id}`);
  }

  public addFood(food: any) {
    return this.http.post<any>(`${this.baseUrl}/food`, food);
  }

  public updateFood(id: number, food: any) {
    return this.http.put<any>(`${this.baseUrl}/food/${id}`, food);
  }

  public deleteFood(id: number) {
    return this.http.delete<any>(`${this.baseUrl}/food/${id}`);
  }
}
