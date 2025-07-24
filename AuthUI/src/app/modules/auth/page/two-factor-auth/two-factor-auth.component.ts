import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';

@Component({
  selector: 'app-two-factor-auth',
  standalone: false,
  templateUrl: './two-factor-auth.component.html',
  styleUrl: './two-factor-auth.component.css'
})
export class TwoFactorAuthComponent {
  code = '';
  error: string | null = null;
  loading = false;
  email: string | null = null;

  constructor(private router: Router, private authService: AuthService, private toastService: ToastService) {
    this.email = sessionStorage.getItem('2fa_email');
    if (!this.email) {
      this.router.navigate(['/auth/login']);
    }
  }

  onVerify() {
    this.error = null;
    this.loading = true;
    if (!this.email) {
      this.error = 'No email found for verification.';
      this.loading = false;
      return;
    }
    this.authService.verifyTwoFactorToken(this.email, this.code).subscribe({
      next: (res) => {
        this.loading = false;
        if (res && res.data.token) {
          localStorage.setItem('auth_token', res.data.token);
        }
        this.toastService.success('Two-factor authentication successful!');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.loading = false;
        this.error = err || 'Verification failed. Please try again.';
        this.toastService.error(err || 'Verification failed. Please try again.');
      }
    });
  }

  preventNonNumeric(event: KeyboardEvent) {
    // Allow: Backspace, Tab, Arrow keys, Delete
    if (["Backspace", "Tab", "ArrowLeft", "ArrowRight", "Delete"].includes(event.key)) {
      return;
    }
    // Allow paste
    if (event.ctrlKey && (event.key === 'v' || event.key === 'V')) {
      return;
    }
    // Block if not a number
    if (!/^[0-9]$/.test(event.key)) {
      event.preventDefault();
    }
  }

  onPaste(event: ClipboardEvent) {
    const pasted = event.clipboardData?.getData('text') ?? '';
    if (!/^[0-9]*$/.test(pasted)) {
      event.preventDefault();
    }
  }
}
