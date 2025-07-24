import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  standalone: false,
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent {
  userName = '';
  firstName = '';
  lastName = '';
  email = '';
  gender = '';
  enable2FA = false;
  error: string | null = null;
  loading = false;
  isAdmin = false;

  constructor(private authService: AuthService, private toastService: ToastService, private router: Router) {
    const token = localStorage.getItem('auth_token');
    if (token) {
      const payload = this.decodeJwt(token);
      const roles = payload['roles'] || payload['role'] || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      if (Array.isArray(roles)) {
        this.isAdmin = roles.includes('Admin');
      } else if (typeof roles === 'string') {
        this.isAdmin = roles === 'Admin' || roles.split(',').includes('Admin');
      }
    }
  }

  private decodeJwt(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload.replace(/-/g, '+').replace(/_/g, '/')));
    } catch {
      return {};
    }
  }

  onCreate() {
    this.error = null;
    if (!this.userName.trim()) {
      this.error = 'User name is required.';
      this.toastService.error(this.error);
      return;
    }
    if (!this.firstName.trim()) {
      this.error = 'First name is required.';
      this.toastService.error(this.error);
      return;
    }
    if (!this.lastName.trim()) {
      this.error = 'Last name is required.';
      this.toastService.error(this.error);
      return;
    }
    if (!this.email.trim()) {
      this.error = 'Email is required.';
      this.toastService.error(this.error);
      return;
    }
    // Email regex validation
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(this.email)) {
      this.error = 'Please enter a valid email address.';
      this.toastService.error(this.error);
      return;
    }
    if (!this.gender) {
      this.error = 'Gender is required.';
      this.toastService.error(this.error);
      return;
    }
    this.loading = true;
    this.authService.createUser({
      userName: this.userName,
      email: this.email,
      firstName: this.firstName,
      lastName: this.lastName,
      gender: this.gender,
      enable2FA: this.enable2FA
    }).subscribe({
      next: (res) => {
        this.loading = false;
        this.toastService.success('User created successfully!');
        this.userName = this.firstName = this.lastName = this.email = this.gender = '';
        this.enable2FA = false;
        // Redirect to dashboard after a short delay
        setTimeout(() => {
          this.router.navigate(['/dashboard']);
        }, 1500);
      },
      error: (err) => {
        this.loading = false;
        this.error = err || 'Failed to create user.';
        this.toastService.error(err || 'Failed to create user.');
      }
    });
  }
}
