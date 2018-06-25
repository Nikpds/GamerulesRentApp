import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError as observableThrowError, Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Customer } from '../app.model';
import { PagedData } from '../shared/pagination.service';
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private customerUrl = environment.api + '/customer';

  constructor(
    private http: HttpClient
  ) { }

  insertCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(`${this.customerUrl}`, customer)
      .pipe(catchError(this.errorHandler));
  }

  getCustomer(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.customerUrl}/${id}`)
      .pipe(catchError(this.errorHandler));
  }
  getCustomers(data: PagedData<Customer>): Observable<PagedData<Customer>> {
    return this.http.post<PagedData<Customer>>(`${this.customerUrl}/all`, data)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    return observableThrowError(error.error || 'Server Error');
  }
}
