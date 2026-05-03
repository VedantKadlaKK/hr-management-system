import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule,
            MatButtonModule, MatCardModule, MatIconModule, MatSelectModule,
            MatSnackBarModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  form: FormGroup;
  isLoading = false;
  hidePassword = true;

  roles = ['Employee'];

  constructor(
    private fb: FormBuilder,
    public authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.form = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      role: ['Employee', Validators.required]
    });

    if (this.authService.canCreatePrivilegedUsers()) {
      this.roles = ['Admin', 'HR', 'Employee'];
    }
  }

  submit() {
    if (this.form.invalid) return;
    this.isLoading = true;
    const persistSession = !this.authService.isLoggedIn();

    this.authService.register(this.form.value, persistSession).subscribe({
      next: () => {
        this.isLoading = false;
        if (persistSession) {
          this.router.navigate(['/dashboard']);
          return;
        }

        this.snackBar.open('User created successfully', 'Close', { duration: 3000 });
        this.form.reset({ role: 'Employee' });
      },
      error: (err) => {
        this.isLoading = false;
        this.snackBar.open(err.error?.message || 'Registration failed', 'Close', { duration: 3000 });
      }
    });
  }
}
