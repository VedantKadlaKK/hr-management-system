import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { LeaveRequestService } from '../../../core/services/leave-request.service';
import { LeaveRequest } from '../../../core/models/leave-request.model';

@Component({
  selector: 'app-leave-list',
  standalone: true,
  imports: [CommonModule, RouterLink, MatTableModule, MatButtonModule,
            MatIconModule, MatCardModule, MatChipsModule, MatSnackBarModule,
            MatTooltipModule],
  templateUrl: './leave-list.component.html',
  styleUrl: './leave-list.component.scss'
})
export class LeaveListComponent implements OnInit {
  leaves: LeaveRequest[] = [];
  displayedColumns = ['employee', 'leaveType', 'startDate', 'endDate', 'reason', 'status', 'actions'];

  constructor(
    private leaveService: LeaveRequestService,
    private snackBar: MatSnackBar,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadLeaves();
  }

  loadLeaves() {
    this.leaveService.getAll().subscribe(data => {
      this.leaves = data;
      this.cdr.detectChanges();
    });
  }

  approve(id: number) {
    this.leaveService.updateStatus(id, { status: 1 }).subscribe(() => {
      this.snackBar.open('Leave approved!', 'Close', { duration: 3000 });
      this.loadLeaves();
    });
  }

  reject(id: number) {
    const comment = prompt('Enter rejection reason:');
    if (comment !== null) {
      this.leaveService.updateStatus(id, { status: 2, rejectionComment: comment }).subscribe(() => {
        this.snackBar.open('Leave rejected!', 'Close', { duration: 3000 });
        this.loadLeaves();
      });
    }
  }

  delete(id: number) {
    if (confirm('Delete this leave request?')) {
      this.leaveService.delete(id).subscribe(() => {
        this.snackBar.open('Deleted!', 'Close', { duration: 3000 });
        this.loadLeaves();
      });
    }
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Approved': return 'chip-approved';
      case 'Rejected': return 'chip-rejected';
      case 'Pending': return 'chip-pending';
      default: return '';
    }
  }
}
