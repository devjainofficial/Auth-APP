import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthStateService } from '../services/auth-state.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authStateService: AuthStateService) {}
  
  canActivate(): boolean {
    const token = localStorage.getItem('auth_token');
    if (!token) {
      this.authStateService.setAuthenticated(false);
      this.router.navigate(['/auth/login']);
      return false;
    }
    this.authStateService.setAuthenticated(true);
    return true;
  }
}

@Injectable({ providedIn: 'root' })
export class LoginRedirectGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('auth_token');
    if (token) {
      this.router.navigate(['/dashboard']);
      return false;
    }
    return true;
  }
} 