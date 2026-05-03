import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Department {
  id: number;
  name: string;
  description: string;
}

@Injectable({ providedIn: 'root' })
export class DepartmentService {
  private apiUrl = 'http://localhost:5019/api/departments';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrl);
  }
}