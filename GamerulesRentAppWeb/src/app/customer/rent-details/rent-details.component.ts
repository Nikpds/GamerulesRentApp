import { Component, OnInit } from '@angular/core';
import { BoardGameRental } from '../../app.model';

import { CustomerService } from '../customer.service';
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
    private customerService: CustomerService
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
      console.log(res);
    });
  }

  addCustomer(customer: any) {
    this.customers = new Array<any>();
    this.rent.customerId = customer.id;
    this.rentName = customer.fullname;
  }
}
