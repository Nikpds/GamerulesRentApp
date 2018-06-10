import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';

import { CustomerService } from './customer.service';

const routes: Route[] = [
  { path: 'customers', component: CustomerListComponent },
  { path: 'customer/:id', component: CustomerDetailsComponent },
  { path: 'customer/new', component: CustomerDetailsComponent },
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    CustomerListComponent,
    CustomerDetailsComponent
  ],
  providers: [
    CustomerService
  ]
})
export class CustomerModule { }
