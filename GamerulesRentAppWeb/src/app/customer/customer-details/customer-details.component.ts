import { Component, OnInit } from '@angular/core';

import { Customer } from '../../app.model';

import { CustomerService } from '../customer.service';
import { NotifyService } from '../../shared/notify.service';
@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.sass']
})
export class CustomerDetailsComponent implements OnInit {
  customer: Customer;
  constructor(
    private customerService: CustomerService,
    private notify: NotifyService
  ) { }

  ngOnInit() {
    this.customer = new Customer();
  }

  insert() {
    this.customerService.insertCustomer(this.customer).subscribe(res => {
      this.customer = res;
      this.notify.success();
    }, err => {
      this.notify.error(err.error);
    });
  }
}
