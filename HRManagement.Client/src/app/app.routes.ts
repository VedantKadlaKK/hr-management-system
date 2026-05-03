import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./features/auth/register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'dashboard',
    loadComponent: () =>
      import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard]
  },
  {
    path: 'employees',
    loadComponent: () =>
      import('./features/employees/employee-list/employee-list.component').then(m => m.EmployeeListComponent),
    canActivate: [authGuard]
  },
  {
    path: 'employees/new',
    loadComponent: () =>
      import('./features/employees/employee-form/employee-form.component').then(m => m.EmployeeFormComponent),
    canActivate: [authGuard]
  },
  {
    path: 'employees/edit/:id',
    loadComponent: () =>
      import('./features/employees/employee-form/employee-form.component').then(m => m.EmployeeFormComponent),
    canActivate: [authGuard]
  },
  {
    path: 'leaves',
    loadComponent: () =>
      import('./features/leave-requests/leave-list/leave-list.component').then(m => m.LeaveListComponent),
    canActivate: [authGuard]
  },
  {
    path: 'leaves/new',
    loadComponent: () =>
      import('./features/leave-requests/leave-form/leave-form.component').then(m => m.LeaveFormComponent),
    canActivate: [authGuard]
  }
];