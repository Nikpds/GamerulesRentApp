import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { BoardGameRental } from '../../app.model';
import { Location } from '@angular/common';

import { CustomerService } from '../customer.service';
import { DataResponse, PaginationService } from '../../shared/pagination.service';
import { NotifyService } from '../../shared/notify.service';
import { LoaderService } from '../../shared/loader.service';
@Component({
  selector: 'app-rent-list',
  templateUrl: './rent-list.component.html',
  styleUrls: ['./rent-list.component.sass']
})
export class RentListComponent implements OnInit {
  data = new DataResponse<BoardGameRental>();
  page = 1;
  pages = new Array<number>();
  pageSize = 5;
  totalPages = 0;
  status = 'all';
  order = '';
  search = '';
  constructor(
    private pagerService: PaginationService,
    private customerService: CustomerService,
    private notify: NotifyService,
    private activeRoute: ActivatedRoute,
    private router: Router,
    private loader: LoaderService,
    private location: Location
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe((param: Params) => {
      this.page = param['page'];
      this.pageSize = param['pageSize'];
      this.status = param['status'];
      this.order = param['order'];
      this.search = param['search'];
      this.getRents();
    });

  }

  getRents() {
    this.loader.show();
    this.changeRoute();
    this.customerService.getPagedRentals(this.page, this.pageSize,
      this.status, this.order, this.search).subscribe(res => {
        this.data = res;
        this.totalPages = Math.ceil(res.totalRows / this.pageSize);
        this.pages = this.pagerService.getPages(this.totalPages, this.page);
        console.log(res);
        this.loader.hide();
      }, error => {
        this.loader.hide();
        this.notify.error(error);
      });
  }

  setPage(p: number) {
    if (p === this.page) {
      return;
    }
    this.page = p;
    this.getRents();
  }

  nextPage() {
    if (this.page === this.totalPages) {
      return;
    }
    this.page += 1;
    this.getRents();
  }

  previousPage() {
    if (this.page === 1) {
      return;
    }
    this.page -= 1;
    this.getRents();
  }

  firstPage() {
    if (this.page === 1) {
      return;
    }
    this.page = 1;
    this.getRents();
  }

  lastPage() {
    if (this.page === this.totalPages) {
      return;
    }
    this.page = this.totalPages;
    this.getRents();
  }

  orderBy(order: string) {
    if (this.order === 'lastname_asc' && order === 'lastname_asc') {
      this.order = 'lastname_desc';
    } else if (this.order === 'rent_asc' && order === 'rent_asc') {
      this.order = 'rent_desc';
    } else if (this.order === 'return_asc' && order === 'return_asc') {
      this.order = 'return_desc';
    } else {
      this.order = order;
    }
    console.log(this.order);
    this.getRents();
  }

  changeRoute() {
    const search = this.search ? `/${this.search}` : '';
    this.location.replaceState(`/rents/${this.page}/${this.pageSize}/${this.status}/${this.order}${search}`);
  }

}
