import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PaginationParams } from '../../Shared/Models/PaginationParams';
import { Pagination } from '../../Shared/Models/Pagination';

export class NutritionGenericService<T> {
  protected http = inject(HttpClient);

  constructor(
    protected baseUrl: string,
    protected entityName: string,
  ) {}

  getAll(paginationParams: PaginationParams) {
    let params = new HttpParams();

    params = params.append('pageNumber', paginationParams.pagenumber);
    params = params.append('pageSize', paginationParams.pagesize);

    if (paginationParams.sort) {
      params = params.append('sort', paginationParams.sort);
    }

    return this.http.get<Pagination<T>>(`${this.baseUrl}/${this.entityName}?${params}`);
  }

  getById(id: number) {
    return this.http.get<T>(`${this.baseUrl}/${this.entityName}/${id}`);
  }

  add(entity: T) {
    return this.http.post<T>(`${this.baseUrl}/${this.entityName}`, entity);
  }

  update(id: number, entity: T) {
    return this.http.put<T>(`${this.baseUrl}/${this.entityName}/${id}`, entity);
  }

  delete(id: number) {
    return this.http.delete<T>(`${this.baseUrl}/${this.entityName}/${id}`);
  }
}
