import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Customer } from '../app.model';
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private customerUrl = environment.api + '/customer';

  constructor(
    private http: HttpClient
  ) { }

  insertCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(`${this.customerUrl}`, customer);
  }

  getCustomers(): Observable<Array<Customer>> {
    return this.http.get<Array<Customer>>(`${this.customerUrl}/all`);
  }
}
