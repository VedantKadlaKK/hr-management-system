import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { EmployeeService } from '../../core/services/employee.service';
import { LeaveRequestService } from '../../core/services/leave-request.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  totalEmployees = 0;
  activeEmployees = 0;
  pendingLeaves = 0;
  approvedLeaves = 0;

  constructor(
    private employeeService: EmployeeService,
    private leaveService: LeaveRequestService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.employeeService.getAll().subscribe(employees => {
      this.totalEmployees = employees.length;
      this.activeEmployees = employees.filter(e => e.isActive).length;
      this.cdr.detectChanges();
    });

    this.leaveService.getAll().subscribe(leaves => {
      this.pendingLeaves = leaves.filter(l => l.status === 'Pending').length;
      this.approvedLeaves = leaves.filter(l => l.status === 'Approved').length;
      this.cdr.detectChanges();
    });
  }
}