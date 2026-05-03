import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { LeaveRequestService } from '../../../core/services/leave-request.service';
import { EmployeeService } from '../../../core/services/employee.service';
import { Employee } from '../../../core/models/employee.model';

@Component({
  selector: 'app-leave-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule,
            MatSelectModule, MatButtonModule, MatDatepickerModule, MatNativeDateModule,
            MatSnackBarModule, MatCardModule, MatIconModule, RouterLink],
  templateUrl: './leave-form.component.html',
  styleUrl: './leave-form.component.scss'
})
export class LeaveFormComponent implements OnInit {
  form!: FormGroup;
  employees: Employee[] = [];

  leaveTypes = [
    { value: 0, label: 'Annual' },
    { value: 1, label: 'Sick' },
    { value: 2, label: 'Casual' },
    { value: 3, label: 'Maternity' },
    { value: 4, label: 'Unpaid' }
  ];

  constructor(
    private fb: FormBuilder,
    private leaveService: LeaveRequestService,
    private employeeService: EmployeeService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      employeeId: ['', Validators.required],
      leaveType: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      reason: ['', [Validators.required, Validators.minLength(10)]]
    });

    this.employeeService.getAll().subscribe(e => this.employees = e);
  }

  submit() {
    if (this.form.invalid) return;
    this.leaveService.create(this.form.value).subscribe(() => {
      this.snackBar.open('Leave request submitted!', 'Close', { duration: 3000 });
      this.router.navigate(['/leaves']);
    });
  }
}