import { NgModule } from '@angular/core';
import { LoginComponent } from 'src/app/routes/login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    SharedModule,
    ReactiveFormsModule
  ],
  exports: [
  ]

})
export class LoginModule { }
