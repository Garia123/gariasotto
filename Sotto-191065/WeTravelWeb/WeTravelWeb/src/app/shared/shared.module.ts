import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './navbar/navbar.component';
import { PrimengModule } from './primeng.module';

@NgModule({
  declarations: [NavBarComponent],
  imports: [
    CommonModule,
    PrimengModule
  ],
  exports: [
    NavBarComponent,
    CommonModule,
    PrimengModule
  ]

})
export class SharedModule { }
