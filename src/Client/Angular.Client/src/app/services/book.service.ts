import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Book, BookWithReviews} from "../models/book";
import {bookEndpoints} from "../constants/endpoints";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  constructor(private httpClient: HttpClient) {}

  getLastUpdatedBooks(count: number): Observable<Book[]> {
    return this.httpClient.get<Book[]>(bookEndpoints.books + `?pageNumber=1&pageSize=${count}`);
  }

  getBookById(id: number): Observable<BookWithReviews> {
    return this.httpClient.get<BookWithReviews>(bookEndpoints.books + `/${id}`);
  }
}
