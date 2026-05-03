import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../../../core/services/employee.service';
import { Employee } from '../../../core/models/employee.model';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule,
            MatIconModule, MatCardModule, MatChipsModule, MatSnackBarModule],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  displayedColumns = ['name', 'email', 'position', 'department', 'salary', 'status', 'actions'];

  constructor(
    private employeeService: EmployeeService,
    private snackBar: MatSnackBar,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getAll().subscribe(data => {
      this.employees = data;
      this.cdr.detectChanges();
    });
  }

  delete(id: number) {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.delete(id).subscribe(() => {
        this.snackBar.open('Employee deleted successfully', 'Close', { duration: 3000 });
        this.loadEmployees();
      });
    }
  }
}