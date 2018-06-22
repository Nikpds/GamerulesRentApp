import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { ToastrModule } from 'ngx-toastr';

import { NavbarComponent } from '../layout/navbar/navbar.component';
import { NotifyService } from './notify.service';

const config = {
  autoDismiss: true,
  positionClass: 'toast-bottom-right',
  preventDuplicates: true
};

@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    FormsModule,
    ToastrModule.forRoot(config),
    HttpClientModule,
    RouterModule
  ],
  declarations: [
    NavbarComponent
  ], providers: [
    NotifyService
  ], exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NavbarComponent
  ]
})
export class SharedModule { }
