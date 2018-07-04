import { Component, OnInit } from '@angular/core';
import { BoardGameRental } from '../../app.model';

import { CustomerService } from '../customer.service';
import { LoaderService } from '../../shared/loader.service';
import { NotifyService } from '../../shared/notify.service';
@Component({
  selector: 'app-rent-details',
  templateUrl: './rent-details.component.html',
  styleUrls: ['./rent-details.component.sass']
})
export class RentDetailsComponent implements OnInit {
  boardGameName: string;
  rentName: string;
  customers = new Array<any>();
  rent = new BoardGameRental();
  constructor(
    private customerService: CustomerService,
    private loader: LoaderService,
    private notify: NotifyService
  ) { }

  ngOnInit() {
    this.rent.rentDate = new Date();
  }

  addBoardGame() {
    if (!this.boardGameName && !this.boardGameName.trim()) { return; }
    this.rent.boardGames.push(this.boardGameName);
    this.boardGameName = null;
  }

  removeBoardGame(i: number) {
    this.rent.boardGames.splice(i, 1);
  }

  searchCustomers() {
    if (!this.rentName && !this.rentName.trim()) {
      this.customers = new Array<any>();
      return;
    }
    this.customerService.getCustomerForRent(this.rentName).subscribe(res => {
      this.customers = res;
      if (this.customers.length === 0) {
        this.notify.info('Δεν βρέθηκε χρήστης με τα στοιχεία που δώσατε');
      }
    });
  }

  rentBoard() {
    this.loader.show();
    this.customerService.rentBoardGame(this.rent).subscribe(res => {
      this.rent = res;
      this.loader.hide();
    }, err => this.loader.hide());
  }

  deleteRent() {
    this.loader.show();
  }

  completeRent() {
    this.loader.show();
  }

  addCustomer(customer: any) {
    this.customers = new Array<any>();
    this.rent.customerId = customer.id;
    this.rentName = customer.fullname;
  }
}
