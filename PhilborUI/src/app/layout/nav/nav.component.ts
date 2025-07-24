import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';
import { Router } from '@angular/router';
import { AuthStateService } from '../../core/services/auth-state.service';

@Component({
  selector: 'app-nav',
  standalone: false,
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  constructor(
    private authService: AuthService, 
    private toastService: ToastService, 
    private router: Router,
    private authStateService: AuthStateService
  ) {}

  logout() {
    this.authStateService.logout();
    this.toastService.info('You have been logged out successfully.');
    this.router.navigate(['/auth/login']);
  }
}
