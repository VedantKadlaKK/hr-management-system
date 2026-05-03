import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveRequest, CreateLeaveRequest, UpdateLeaveStatus } from '../models/leave-request.model';

@Injectable({ providedIn: 'root' })
export class LeaveRequestService {
  private apiUrl = 'http://localhost:5019/api/leaverequests';

  constructor(private http: HttpClient) {}

  getAll(): Observable<LeaveRequest[]> {
    return this.http.get<LeaveRequest[]>(this.apiUrl);
  }

  getById(id: number): Observable<LeaveRequest> {
    return this.http.get<LeaveRequest>(`${this.apiUrl}/${id}`);
  }

  getByEmployee(employeeId: number): Observable<LeaveRequest[]> {
    return this.http.get<LeaveRequest[]>(`${this.apiUrl}/employee/${employeeId}`);
  }

  create(leave: CreateLeaveRequest): Observable<LeaveRequest> {
    return this.http.post<LeaveRequest>(this.apiUrl, leave);
  }

  updateStatus(id: number, status: UpdateLeaveStatus): Observable<LeaveRequest> {
    return this.http.put<LeaveRequest>(`${this.apiUrl}/${id}/status`, status);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}