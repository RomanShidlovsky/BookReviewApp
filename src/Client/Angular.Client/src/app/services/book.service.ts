import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {bookEndpoints} from "../constants/endpoints";
import {BookResponseDto, BookWithReviewsResponseDto} from "../models/book/bookResponseDto";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  constructor(private httpClient: HttpClient) {}

  getLastUpdatedBooks(count: number): Observable<BookResponseDto[]> {
    return this.httpClient.get<BookResponseDto[]>(bookEndpoints.books + `?pageNumber=1&pageSize=${count}`);
  }

  getBookById(id: number): Observable<BookWithReviewsResponseDto> {
    return this.httpClient.get<BookWithReviewsResponseDto>(bookEndpoints.books + `/${id}`);
  }
}
