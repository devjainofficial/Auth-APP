import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { LoginResponse } from '../../data/schema/user';
import { AuthStateService } from './auth-state.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7222/api/auth/login-user';

  constructor(private http: HttpClient, private authStateService: AuthStateService) {}

  login(email: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, { email, password }).pipe(
      map(response => {
        if (response.isSuccess && response.data.token) {
          localStorage.setItem('auth_token', response.data.token);
          this.authStateService.setAuthenticated(true);
        }
        return response;
      }),
      catchError(this.handleError)
    );
  }

  verifyTwoFactorToken(email: string, token: string): Observable<any> {
    const url = `https://localhost:7222/api/auth/verify-twoFactor-token?Email=${encodeURIComponent(email)}&VerificationToken=${encodeURIComponent(token)}`;
    return this.http.get<any>(url).pipe(
      map(response => {
        if (response && response.token) {
          localStorage.setItem('auth_token', response.token);
          this.authStateService.setAuthenticated(true);
        }
        return response;
      }),
      catchError(this.handleError)
    );
  }

  acceptInvitation(data: { email: string; token: string; firstName: string; lastName: string; password: string }): Observable<any> {
    const url = 'https://localhost:7222/api/auth/accept-invitation';
    return this.http.post<any>(url, data).pipe(
      catchError(this.handleError)
    );
  }

  forgotPassword(email: string): Observable<any> {
    const url = `https://localhost:7222/api/auth/forgot-password?Email=${encodeURIComponent(email)}`;
    return this.http.get<any>(url).pipe(
      catchError(this.handleError)
    );
  }

  resetPassword(email: string, resetPasswordToken: string, password: string): Observable<any> {
    const url = `https://localhost:7222/api/auth/reset-password?Email=${encodeURIComponent(email)}&ResetPasswordToken=${encodeURIComponent(resetPasswordToken)}&Password=${encodeURIComponent(password)}`;
    return this.http.get<any>(url).pipe(
      catchError(this.handleError)
    );
  }

  createUser(data: { userName: string; email: string; firstName: string; lastName: string; gender: string; enable2FA: boolean }): Observable<any> {
    const url = 'https://localhost:7222/api/user/create';
    return this.http.post<any>(url, data).pipe(
      catchError(this.handleError)
    );
  }

  logout() {
    localStorage.removeItem('auth_token');
  }

  private handleError(error: HttpErrorResponse) {
    let errorMsg = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      errorMsg = `Error: ${error.error.message}`;
    } else if (error.error.error && error.error.error.description) {
      errorMsg = error.error.error.description;
    }
    return throwError(() => errorMsg);
  }
} 