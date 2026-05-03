import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export interface LoginDto { email: string; password: string; }
export interface RegisterDto { email: string; password: string; fullName: string; role: string; }
export interface AuthResponse { token: string; email: string; fullName: string; role: string; expiry: string; }

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5019/api/auth';

  constructor(private http: HttpClient) {}

  login(dto: LoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, dto).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('user', JSON.stringify(res));
      })
    );
  }

  register(dto: RegisterDto, persistSession = true): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, dto).pipe(
      tap(res => {
        if (persistSession) {
          localStorage.setItem('token', res.token);
          localStorage.setItem('user', JSON.stringify(res));
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUser(): AuthResponse | null {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }

  getRole(): string | null {
    return this.getUser()?.role ?? null;
  }

  hasRole(...roles: string[]): boolean {
    const role = this.getRole();
    return !!role && roles.includes(role);
  }

  canManageEmployees(): boolean {
    return this.hasRole('Admin', 'HR');
  }

  canApproveLeaves(): boolean {
    return this.hasRole('Admin', 'HR');
  }

  canCreatePrivilegedUsers(): boolean {
    return this.hasRole('Admin');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
