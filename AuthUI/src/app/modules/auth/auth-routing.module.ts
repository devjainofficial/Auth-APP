import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './page/login/login.component';
import { TwoFactorAuthComponent } from './page/two-factor-auth/two-factor-auth.component';
import { AcceptInvitationComponent } from './page/accept-invitation/accept-invitation.component';
import { ForgotPasswordComponent } from './page/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './page/reset-password/reset-password.component';
import {  LoginRedirectGuard } from '../../core/guards/auth.guard';

const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [LoginRedirectGuard]},
  {path: 'two-factor-auth', component: TwoFactorAuthComponent},
  {path: 'invitation', component: AcceptInvitationComponent},
  {path: 'forgot-password', component: ForgotPasswordComponent},
  {path: 'reset-password', component: ResetPasswordComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
