import { Component, OnInit } from '@angular/core';

import { CustomerService } from '../../customer/customer.service';
import { LoaderService } from '../../shared/loader.service';
import { NotifyService } from '../../shared/notify.service';
import { DashboardView } from '../../app.model';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  info = new DashboardView();
  constructor(
    private notify: NotifyService,
    private loader: LoaderService,
    private customerService: CustomerService
  ) {

  }
  ngOnInit() {
    this.getDashboardInfo();
  }

  getDashboardInfo() {
    this.loader.show();
    this.customerService.getDashboardInfo().subscribe(res => {
      this.info = res;
      console.log(res);
      this.loader.hide();
    }, err => {
      this.loader.hide();
    });
  }

}
