import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../Shared/Models/Pagination';
import { Food } from '../../Shared/Models/Food';
import { PaginationParams } from '../../Shared/Models/PaginationParams';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/nutrition';

  public getFoods(paginationParams: PaginationParams) {
    let params = new HttpParams();

    params = params.append('pageNumber', paginationParams.pagenumber);
    params = params.append('pageSize', paginationParams.pagesize);

    if (paginationParams.sort) {
      params = params.append('sort', paginationParams.sort);
    }

    return this.http.get<Pagination<Food>>(`${this.baseUrl}/food?${params}`);
  }

  public getFoodById(id: number) {
    return this.http.get<Food>(`${this.baseUrl}/food/${id}`);
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
