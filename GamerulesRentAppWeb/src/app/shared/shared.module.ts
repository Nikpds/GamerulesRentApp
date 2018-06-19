import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { NavbarComponent } from '../layout/navbar/navbar.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule
  ],
  declarations: [
    NavbarComponent
  ], exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NavbarComponent
  ]
})
export class SharedModule { }
