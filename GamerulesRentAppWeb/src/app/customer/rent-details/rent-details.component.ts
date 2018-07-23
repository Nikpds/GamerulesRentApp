import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
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
  verify: boolean;
  rentName: string;
  customers = new Array<any>();
  rent: BoardGameRental;
  constructor(
    private customerService: CustomerService,
    private loader: LoaderService,
    private activeRoute: ActivatedRoute,
    private notify: NotifyService,
    private router: Router
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe((param: Params) => {
      const id = param['id'];
      if (id !== 'new') {
        this.getRent(id);
      } else {
        this.rent = new BoardGameRental();
        this.rent.rentDate = new Date();
        this.rentName = '';
      }
    });
  }

  getRent(id: string) {
    this.loader.show();
    this.customerService.getBoardGameRental(id).subscribe(res => {
      this.rent = res;
      console.log(res);
      this.loader.hide();
    }, error => {
      this.loader.hide();
    });
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
      this.notify.success('Η Ενοικίαση αποθηκέυτηκε επιτυχώς');
      this.router.navigate(['/rent', res.id]);
    }, err => {
      this.loader.hide();
      this.notify.error(err);
    });
  }
  removeCustomer() {
    this.rentName = '';
    this.searchCustomers();
  }

  deleteRent() {
    this.loader.show();
    this.customerService.deleteRental(this.rent.id).subscribe(res => {
      this.router.navigate(['/rents']);
      this.notify.success('Η Ενοικίαση διαγράφτηκε');
      this.loader.hide();
    }, error => {
      this.loader.hide();
      this.notify.error(error);
    });
  }

  completeRent() {
    this.loader.show();
    this.customerService.returnBoardGame(this.rent.id).subscribe(res => {
      this.rent = res;
      this.notify.success('Η Ενοικίαση ολοκληρώθηκε');
      this.loader.hide();
    }, error => {
      this.notify.error('Σφάλμα. Τα γάμησες Σταθάκη');
      this.loader.hide();
    });
  }

  addCustomer(customer: any) {
    this.customers = new Array<any>();
    this.rent.customerId = customer.id;
    this.rentName = customer.fullname;
  }

  updateRent() {
    this.loader.show();
    this.customerService.updateRentBoardGame(this.rent).subscribe(res => {
      this.rent = res;
      this.loader.hide();
      this.notify.success('Επιτυχής Επεξεργασία');
    }, err => {
      this.notify.error('Σφάλμα. Τα γάμησες Σταθάκη');
      this.loader.hide();
    });
  }
}
