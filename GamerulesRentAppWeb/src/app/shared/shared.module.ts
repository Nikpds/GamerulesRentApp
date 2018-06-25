import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { ToastrModule } from 'ngx-toastr';
import { OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_FORMATS, OWL_DATE_TIME_LOCALE, OwlDateTimeIntl } from 'ng-pick-datetime';

import { NavbarComponent } from '../layout/navbar/navbar.component';
import { NotifyService } from './notify.service';
import { PaginationService } from './pagination.service';
import { LoaderService } from './loader.service';
import { ModalDirective } from './modal.directive';

export const MY_NATIVE_FORMATS = {
  fullPickerInput: { year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: 'numeric' }
};
export const DefaultIntl = {
  cancelBtnLabel: 'Κλείσιμο', setBtnLabel: 'Επιλογή'
};

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
    RouterModule,
    OwlNativeDateTimeModule,
    OwlDateTimeModule
  ],
  declarations: [
    NavbarComponent,
    ModalDirective
  ], providers: [
    { provide: OWL_DATE_TIME_FORMATS, useValue: MY_NATIVE_FORMATS },
    { provide: OWL_DATE_TIME_LOCALE, useValue: 'el' },
    { provide: OwlDateTimeIntl, useValue: DefaultIntl },
    NotifyService,
    LoaderService,
    PaginationService
  ], exports: [
    CommonModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    FormsModule,
    HttpClientModule,
    NavbarComponent,
  ]
})
export class SharedModule { }
