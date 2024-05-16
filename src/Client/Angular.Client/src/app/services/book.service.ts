import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Book} from "../models/book";
import {bookEndpoints} from "../constants/endpoints";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  constructor(private httpClient: HttpClient) {}

  getLastUpdatedBooks(count: number): Observable<Book[]> {
    return this.httpClient.get<Book[]>(bookEndpoints.pagedBooks + `?pageNumber=1&pageSize=${count}`);
  }
}
