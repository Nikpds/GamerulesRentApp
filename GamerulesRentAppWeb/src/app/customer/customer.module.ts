import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';

import { CustomerService } from './customer.service';
import { RentDetailsComponent } from './rent-details/rent-details.component';
import { RentListComponent } from './rent-list/rent-list.component';

const routes: Route[] = [
  { path: 'customers', component: CustomerListComponent },
  { path: 'customer/:id', component: CustomerDetailsComponent },
  { path: 'customer/new', component: CustomerDetailsComponent },
  { path: 'rents/:page/:pageSize/:status/:order/:search', component: RentListComponent },
  { path: 'rents/:page/:pageSize/:status/:order', component: RentListComponent },
  { path: 'rent/:id', component: RentDetailsComponent },
  { path: 'rent/new', component: RentDetailsComponent }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
  ],
  declarations: [
    CustomerListComponent,
    CustomerDetailsComponent,
    RentDetailsComponent,
    RentListComponent
  ],
  providers: [
    CustomerService
  ]
})
export class CustomerModule { }
