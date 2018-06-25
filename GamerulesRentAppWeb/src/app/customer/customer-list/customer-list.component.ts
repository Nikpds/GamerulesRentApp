import { Component, OnInit } from '@angular/core';

import { Customer } from '../../app.model';

import { CustomerService } from '../customer.service';
import { PagedData, PaginationService } from '../../shared/pagination.service';
import { NotifyService } from '../../shared/notify.service';
@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.sass']
})
export class CustomerListComponent implements OnInit {
  pagedData = new PagedData<Customer>();
  constructor(
    private pagerService: PaginationService,
    private customerService: CustomerService,
    private notify: NotifyService
  ) { }


  ngOnInit() {
    this.init();
  }

  getCustomers(searching: boolean = false) {
    this.customerService.getCustomers(this.pagedData).subscribe(res => {
      this.pagedData = res;
      this.pagedData.page = searching ? 1 : this.pagedData.page;
      this.pagedData.totalPages = Math.ceil(res.totalRows / this.pagedData.pageSize);
      this.pagedData.pages = this.pagerService.getPages(this.pagedData.totalPages, this.pagedData.page);
      this.pagedData.search = '';
    }, error => {
      this.notify.error(error);
    });
  }

  init() {
    this.pagedData.pageSize = 10;
    this.pagedData.page = 1;
    this.pagedData.order = 'lastname';
    this.getCustomers();
  }

  setPage(p: number) {
    if (p === this.pagedData.page) {
      return;
    }
    this.pagedData.page = p;
    this.getCustomers();
  }

  nextPage() {
    if (this.pagedData.page === this.pagedData.totalPages) {
      return;
    }
    this.pagedData.page += 1;
    this.getCustomers();
  }

  previousPage() {
    if (this.pagedData.page === 1) {
      return;
    }
    this.pagedData.page -= 1;
    this.getCustomers();
  }

  firstPage() {
    if (this.pagedData.page === 1) {
      return;
    }
    this.pagedData.page = 1;
    this.getCustomers();
  }

  lastPage() {
    if (this.pagedData.page === this.pagedData.totalPages) {
      return;
    }
    this.pagedData.page = this.pagedData.totalPages;
    this.getCustomers();
  }

  orderBy(order: string) {
    if (order === this.pagedData.order) {
      this.pagedData.isAscending = !this.pagedData.isAscending;
      this.getCustomers();
    }
    this.pagedData.order = order;
    this.getCustomers();
  }

}
