import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
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
    private notify: NotifyService,
    private activeRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {

    this.activeRoute.params.subscribe((param: Params) => {
      const id = param['id'];
      if (id !== 'new') {
        this.getCustomer(id);
      } else {
        this.customer = new Customer();
      }
    });
  }

  insert() {
    this.customerService.insertCustomer(this.customer).subscribe(res => {
      this.customer = res;
      this.notify.success();
      this.router.navigate(['/customer', res.id]);
    }, err => {
      this.notify.error(err.error);
    });
  }

  update() {
    this.customerService.updateCustomer(this.customer).subscribe(res => {
      this.customer = res;
      this.notify.success();
    }, err => {
      this.notify.error(err.error);
    });
  }

  getCustomer(id: string) {
    this.customerService.getCustomer(id).subscribe(res => {
      this.customer = res;
    }, error => {
      this.notify.error(error.message);
    });
  }
}
