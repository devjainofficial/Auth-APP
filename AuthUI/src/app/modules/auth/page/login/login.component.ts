import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  error: string | null = null;
  loading = false;

  constructor(private router: Router, private authService: AuthService, private toastService: ToastService) {}

  onLogin() {
    this.error = null;
    this.loading = true;
    this.authService.login(this.email, this.password).subscribe({
      next: (res) => {
        this.loading = false;
        if (res.isSuccess && res.data) {
          if (res.data.token) {
            localStorage.setItem('auth_token', res.data.token);
          }
          if (!res.data.token && res.data.user.twoFactorEnabled) {
            // Store email in sessionStorage for 2FA step
            sessionStorage.setItem('2fa_email', this.email);
            this.router.navigate(['/auth/two-factor-auth']);
          } else if (res.data.token && !res.data.user.twoFactorEnabled) {
            this.toastService.success('Login successful!');
            this.router.navigate(['/dashboard']);
          } else {
            this.error = res.message || 'Login failed. Please try again.';
          }
        } else {
          this.error = res.message || 'Login failed. Please try again.';
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = err || 'Login failed. Please try again.';
        this.toastService.error(err || 'Login failed. Please try again.');
      }
    });
  }
}
