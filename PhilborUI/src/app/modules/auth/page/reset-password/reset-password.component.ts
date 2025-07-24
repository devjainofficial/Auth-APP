import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  standalone: false,
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  email = '';
  resetPasswordToken = '';
  password = '';
  confirmPassword = '';
  error: string | null = null;
  loading = false;
  success: string | null = null;

  constructor(
    private authService: AuthService,
    private toastService: ToastService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.route.queryParamMap.subscribe(params => {
      this.email = params.get('email') || '';
      this.resetPasswordToken = params.get('token') || params.get('passwordToken') || '';
    });
  }

  onReset() {
    this.error = null;
    this.success = null;
    this.loading = true;
    this.authService.resetPassword(this.email, this.resetPasswordToken, this.password).subscribe({
      next: (res) => {
        this.loading = false;
        this.toastService.success('Password reset successful! You can now log in with your new password.');
        setTimeout(() => this.router.navigate(['/auth/login']), 2000);
      },
      error: (err) => {
        this.loading = false;
        this.error = err || 'Failed to reset password.';
        this.toastService.error(err || 'Failed to reset password.');
      }
    });
  }
}
