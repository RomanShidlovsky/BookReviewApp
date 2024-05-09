import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {BookResponseDto} from "../models/bookResponseDto";
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

  getLastUpdatedBooks(): Observable<BookResponseDto[]> {
    return this.httpClient.get<BookResponseDto[]>(apiUrl +
      `books?pageNumber=${this.pageNumber}&pageSize=${this.pageSize}`);
  }
}
