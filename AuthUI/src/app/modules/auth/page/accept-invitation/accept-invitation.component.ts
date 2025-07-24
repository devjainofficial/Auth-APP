import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../core/services/auth.service';
import { ToastService } from '../../../../core/services/toast.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-accept-invitation',
  standalone: false,
  templateUrl: './accept-invitation.component.html',
  styleUrl: './accept-invitation.component.css'
})
export class AcceptInvitationComponent implements OnInit {
  email = '';
  token = '';
  invitationId = '';
  firstName = '';
  lastName = '';
  password = '';
  confirmPassword = '';
  error: string | null = null;
  loading = false;
  success: string | null = null;

  constructor(private authService: AuthService, private toastService: ToastService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParamMap.subscribe(params => {
      this.email = params.get('email') || '';
      this.token = params.get('token') || '';
      this.invitationId = params.get('invitationId') || '';
    });
  }

  onAccept() {
    this.error = null;
    this.success = null;
    this.loading = true;
    this.authService.acceptInvitation({
      email: this.email,
      token: this.token,
      firstName: this.firstName,
      lastName: this.lastName,
      password: this.password
    }).subscribe({
      next: (res) => {
        this.loading = false;
        this.toastService.success('Invitation accepted! You can now log in.');
        setTimeout(() => this.router.navigate(['/auth/login']), 1500);
      },
      error: (err) => {
        this.loading = false;
        this.error = err || 'Failed to accept invitation.';
        this.toastService.error(err || 'Failed to accept invitation.');
      }
    });
  }
}
