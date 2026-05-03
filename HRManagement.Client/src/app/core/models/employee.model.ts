export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  position: string;
  salary: number;
  joiningDate: string;
  isActive: boolean;
  departmentId: number;
  departmentName: string;
}

export interface CreateEmployee {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  position: string;
  salary: number;
  joiningDate: string;
  departmentId: number;
}

export interface UpdateEmployee {
  firstName: string;
  lastName: string;
  phone: string;
  position: string;
  salary: number;
  isActive: boolean;
  departmentId: number;
}