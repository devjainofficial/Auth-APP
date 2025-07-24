import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor() {
    this.checkAuthenticationStatus();
  }

  checkAuthenticationStatus() {
    const token = localStorage.getItem('auth_token');
    this.isAuthenticatedSubject.next(!!token);
  }

  setAuthenticated(authenticated: boolean) {
    this.isAuthenticatedSubject.next(authenticated);
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.setAuthenticated(false);
  }
} 