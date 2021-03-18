import { NgModule } from '@angular/core';
import { HomeComponent } from 'src/app/routes/home/home.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    SharedModule
  ],
  exports: [
  ]
})
export class HomeModule { }
