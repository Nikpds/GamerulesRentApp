import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError as observableThrowError, Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Customer, BoardGameRental, DashboardView } from '../app.model';
import { PagedData, DataResponse } from '../shared/pagination.service';
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private customerUrl = environment.api + '/customer';
  private boardGameUrl = environment.api + '/boardGameRental';
  private dashboardUrl = environment.api + '/dashboard';
  constructor(
    private http: HttpClient
  ) { }

  insertCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(`${this.customerUrl}`, customer)
      .pipe(catchError(this.errorHandler));
  }
  updateCustomer(customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(`${this.customerUrl}`, customer)
      .pipe(catchError(this.errorHandler));
  }

  deleteCustomer(id: string): Observable<Customer> {
    return this.http.delete<Customer>(`${this.customerUrl}/${id}`)
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

  getCustomerForRent(search: string): Observable<any> {
    return this.http.get<any>(`${this.customerUrl}/rent/${search}`)
      .pipe(catchError(this.errorHandler));
  }

  rentBoardGame(data: BoardGameRental): Observable<BoardGameRental> {
    return this.http.post<BoardGameRental>(`${this.boardGameUrl}`, data)
      .pipe(catchError(this.errorHandler));
  }

  updateRentBoardGame(data: BoardGameRental): Observable<BoardGameRental> {
    return this.http.put<BoardGameRental>(`${this.boardGameUrl}`, data)
      .pipe(catchError(this.errorHandler));
  }

  getBoardGameRental(id: string): Observable<BoardGameRental> {
    return this.http.get<BoardGameRental>(`${this.boardGameUrl}/${id}`)
      .pipe(catchError(this.errorHandler));
  }

  returnBoardGame(id: string): Observable<BoardGameRental> {
    return this.http.get<BoardGameRental>(`${this.boardGameUrl}/return/${id}`)
      .pipe(catchError(this.errorHandler));
  }

  getPagedRentals(page: number, pageSize: number,
    status: string, order: string, search: string): Observable<DataResponse<BoardGameRental>> {
    return this.http.get<DataResponse<BoardGameRental>>
      (`${this.boardGameUrl}/rents/${page}/${pageSize}/${status}/${order}/${search}`)
      .pipe(catchError(this.errorHandler));
  }

  deleteRental(id: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.boardGameUrl}/${id}`)
      .pipe(catchError(this.errorHandler));
  }

  getDashboardInfo(): Observable<DashboardView> {
    return this.http.get<DashboardView>(`${this.dashboardUrl}`)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    return observableThrowError(error.error || 'Server Error');
  }
}
