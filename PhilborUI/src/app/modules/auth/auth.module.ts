import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './page/login/login.component';
import { FormsModule } from '@angular/forms';
import { AcceptInvitationComponent } from './page/accept-invitation/accept-invitation.component';
import { ForgotPasswordComponent } from './page/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './page/reset-password/reset-password.component';
import { TwoFactorAuthComponent } from './page/two-factor-auth/two-factor-auth.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from '../../core/interceptors/token.interceptor';

@NgModule({
  declarations: [
    LoginComponent,
    TwoFactorAuthComponent,
    AcceptInvitationComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    FormsModule,
    HttpClientModule
  ]
})
export class AuthModule { }
