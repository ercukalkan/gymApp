import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/nutrition';

  public getFoods(pagingParams: any) {
    let params = new HttpParams();

    params = params.append('pageNumber', pagingParams.pageNumber);
    params = params.append('pageSize', pagingParams.pageSize);

    if (pagingParams.sort) {
      params = params.append('sort', pagingParams.sort);
    }

    return this.http.get<any>(`${this.baseUrl}/food?${params}`);
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
