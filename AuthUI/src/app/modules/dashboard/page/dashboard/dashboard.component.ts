import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  isAdmin = false;
  userName = '';

  constructor() {
    const token = localStorage.getItem('auth_token');
    if (token) {
      const payload = this.decodeJwt(token);
      this.userName = payload['UserFullName'] || payload['UserName'] || payload['userName'] || '';
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
}
