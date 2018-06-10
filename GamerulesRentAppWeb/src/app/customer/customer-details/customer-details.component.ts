import { Component, OnInit } from '@angular/core';

import { Customer } from '../../app.model';

import { CustomerService } from '../customer.service';
@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.sass']
})
export class CustomerDetailsComponent implements OnInit {
  customer: Customer;
  constructor(
    private customerService: CustomerService
  ) { }

  ngOnInit() {
    this.customer = new Customer();
  }

  insert() {
    this.customerService.insertCustomer(this.customer).subscribe(res => {
      console.log(res);
    });
  }
}
