import { NgModule } from '@angular/core';
import { LodgingsComponent } from 'src/app/components/lodgings/lodgings.component';
import { ReservesComponent } from 'src/app/components/reserves/reserves.component';
import { UsersComponent } from 'src/app/components/users/users.component';
import { AdminComponent } from 'src/app/routes/admin/admin.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    UsersComponent,
    AdminComponent,
    LodgingsComponent,
    ReservesComponent
  ],
  imports: [
    SharedModule
  ],
  exports: [
  ]

})
export class AdminModule { }
