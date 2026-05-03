import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatIconModule } from '@angular/material/icon';
import { EmployeeService } from '../../../core/services/employee.service';
import { DepartmentService, Department } from '../../../core/services/department.service';

@Component({
    selector: 'app-employee-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule,
        MatSelectModule, MatButtonModule, MatDatepickerModule, MatNativeDateModule,
        MatSnackBarModule, MatCardModule, MatSlideToggleModule, MatIconModule],
    templateUrl: './employee-form.component.html',
    styleUrl: './employee-form.component.scss'
})
export class EmployeeFormComponent implements OnInit {
    form!: FormGroup;
    departments: Department[] = [];
    isEditMode = false;
    employeeId?: number;

    constructor(
        private fb: FormBuilder,
        private employeeService: EmployeeService,
        private departmentService: DepartmentService,
        private route: ActivatedRoute,
        private router: Router,
        private snackBar: MatSnackBar
    ) { }

    ngOnInit() {
        this.form = this.fb.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            phone: ['', Validators.required],
            position: ['', Validators.required],
            salary: ['', [Validators.required, Validators.min(0)]],
            joiningDate: ['', Validators.required],
            departmentId: ['', Validators.required],
            isActive: [true]
        });

        this.departmentService.getAll().subscribe(d => this.departments = d);

        this.employeeId = this.route.snapshot.params['id'];
        if (this.employeeId) {
            this.isEditMode = true;
            this.employeeService.getById(this.employeeId).subscribe(emp => {
                this.form.patchValue(emp);
                this.form.get('email')?.disable();
            });
        }
    }

    isSubmitting = false;

    submit() {
        if (this.form.invalid || this.isSubmitting) return;
        this.isSubmitting = true;

        if (this.isEditMode && this.employeeId) {
            this.employeeService.update(this.employeeId, this.form.getRawValue()).subscribe(() => {
                this.snackBar.open('Employee updated!', 'Close', { duration: 3000 });
                this.router.navigate(['/employees']);
            });
        } else {
            this.employeeService.create(this.form.value).subscribe(() => {
                this.snackBar.open('Employee created!', 'Close', { duration: 3000 });
                this.router.navigate(['/employees']);
            });
        }
    }
}