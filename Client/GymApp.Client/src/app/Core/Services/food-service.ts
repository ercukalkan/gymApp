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
}
