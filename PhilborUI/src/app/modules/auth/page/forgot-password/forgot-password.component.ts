import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';

@Component({
  selector: 'app-forgot-password',
  standalone: false,
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  email = '';
  error: string | null = null;
  loading = false;
  success: string | null = null;

  constructor(private authService: AuthService, private toastService: ToastService) {}

  onForgot() {
    this.error = null;
    this.success = null;
    this.loading = true;
    this.authService.forgotPassword(this.email).subscribe({
      next: (res) => {
        this.loading = false;
        this.toastService.success('Password reset link has been sent to your email. Please check your inbox.');
        this.email = ''; // Clear the email field
      },
      error: (err) => {
        this.loading = false;
        this.error = err || 'Failed to send reset email.';
        this.toastService.error(err || 'Failed to send reset email.');
      }
    });
  }
}
