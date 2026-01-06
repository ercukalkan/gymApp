import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ExerciseService {
  private http = inject(HttpClient);
  private baseUrl: string = 'http://localhost:5000/api/gym-tracking';

  public getExercises() {
    return this.http.get<any>(`${this.baseUrl}/exercise`);
  }

  public getExerciseById(id: number) {
    return this.http.get<any>(`${this.baseUrl}/exercise/${id}`);
  }

  public addExercise(exercise: any) {
    return this.http.post<any>(`${this.baseUrl}/exercise`, exercise);
  }

  public updateExercise(id: number, exercise: any) {
    return this.http.put<any>(`${this.baseUrl}/exercise/${id}`, exercise);
  }

  public deleteExercise(id: number) {
    return this.http.delete<any>(`${this.baseUrl}/exercise/${id}`);
  }
}
