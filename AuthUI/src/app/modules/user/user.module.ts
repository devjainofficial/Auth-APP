import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { CreateUserComponent } from './page/create-user/create-user.component';

import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [CreateUserComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    FormsModule
  ],
  exports: [CreateUserComponent]
})
export class UserModule { }
