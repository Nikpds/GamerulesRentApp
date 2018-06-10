import { Component, OnInit } from '@angular/core';

import { Customer } from '../../app.model';

import { CustomerService } from '../customer.service';
@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.sass']
})
export class CustomerListComponent implements OnInit {
  customers = new Array<Customer>();

  constructor(
    private customerService: CustomerService
  ) { }


  ngOnInit() {
    this.getCustomers();
  }

  getCustomers() {
    this.customerService.getCustomers().subscribe(res => {
      this.customers = res;
    });
  }

}
