import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { ToastrModule } from 'ngx-toastr';

import { NavbarComponent } from '../layout/navbar/navbar.component';
import { NotifyService } from './notify.service';
import { PaginationService } from './pagination.service';
import { LoaderService } from './loader.service';
import { ModalDirective } from './modal.directive';

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
    NavbarComponent,
    ModalDirective
  ], providers: [
    NotifyService,
    LoaderService,
    PaginationService
  ], exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NavbarComponent
  ]
})
export class SharedModule { }
