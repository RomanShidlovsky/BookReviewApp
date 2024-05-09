import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Book} from "../models/book";
import {apiUrl} from "../app.config";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  public pageSize: number;
  public pageNumber: number;

  constructor(private httpClient: HttpClient) {
    this.pageNumber = 1;
    this.pageSize = 3;
  }

  getLastUpdatedBooks(): Observable<Book[]> {
    return this.httpClient.get<Book[]>(apiUrl +
      `books?pageNumber=${this.pageNumber}&pageSize=${this.pageSize}`);
  }
}
