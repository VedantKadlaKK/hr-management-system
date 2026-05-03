export interface LeaveRequest {
  id: number;
  startDate: string;
  endDate: string;
  leaveType: string;
  status: string;
  reason: string;
  rejectionComment?: string;
  createdAt: string;
  employeeId: number;
  employeeName: string;
}

export interface CreateLeaveRequest {
  startDate: string;
  endDate: string;
  leaveType: number;
  reason: string;
  employeeId: number;
}

export interface UpdateLeaveStatus {
  status: number;
  rejectionComment?: string;
}