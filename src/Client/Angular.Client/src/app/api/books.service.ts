import {Injectable} from '@angular/core';
import {HttpClient, HttpEvent, HttpHeaders, HttpParams, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {gateway} from "../constants/endpoints";
import {BookResponseDto, BookWithReviewsResponseDto} from "../models/book/bookResponseDto";
import {RemoveAuthorFromBookDto} from "../models/author/removeAuthorFromBookDto";
import {AddAuthorToBookDto} from "../models/author/addAuthorToBookDto";
import {CustomHttpUrlEncodingCodec} from "../encoder";
import {RemoveLanguageFromBookDto} from "../models/language/removeLanguageFromBookDto";
import {AddLanguageToBookDto} from "../models/language/addLanguageToBookDto";
import {UpdateBookDto} from "../models/book/updateBookDto";
import {RemoveSubjectFromBookDto} from "../models/subject/removeSubjectFromBookDto";
import {AddSubjectToBookDto} from "../models/subject/addSubjectToBookDto";
import {CreateBookDto} from "../models/book/createBookDto";

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  protected basePath = gateway;
  public defaultHeaders = new HttpHeaders();

  constructor(protected httpClient: HttpClient) {
  }

  /**
   * @param consumes string[] mime-types
   * @return true: consumes contains 'multipart/form-data', false: otherwise
   */
  private canConsumeForm(consumes: string[]): boolean {
    const form = 'multipart/form-data';
    for (const consume of consumes) {
      if (form === consume) {
        return true;
      }
    }
    return false;
  }


  /**
   *
   *
   * @param filterQueryString
   * @param orderByQueryString
   * @param pageNumber
   * @param pageSize
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksGet(filterQueryString?: string, orderByQueryString?: string, pageNumber?: number, pageSize?: number, observe?: 'body', reportProgress?: boolean): Observable<Array<BookResponseDto>>;
  public apiBooksGet(filterQueryString?: string, orderByQueryString?: string, pageNumber?: number, pageSize?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<BookResponseDto>>>;
  public apiBooksGet(filterQueryString?: string, orderByQueryString?: string, pageNumber?: number, pageSize?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<BookResponseDto>>>;
  public apiBooksGet(filterQueryString?: string, orderByQueryString?: string, pageNumber?: number, pageSize?: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
    if (filterQueryString !== undefined && filterQueryString !== null) {
      queryParameters = queryParameters.set('filterQueryString', <any>filterQueryString);
    }
    if (orderByQueryString !== undefined && orderByQueryString !== null) {
      queryParameters = queryParameters.set('orderByQueryString', <any>orderByQueryString);
    }
    if (pageNumber !== undefined && pageNumber !== null) {
      queryParameters = queryParameters.set('pageNumber', <any>pageNumber);
    }
    if (pageSize !== undefined && pageSize !== null) {
      queryParameters = queryParameters.set('pageSize', <any>pageSize);
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<Array<BookResponseDto>>('get', `${this.basePath}/books`,
      {
        params: queryParameters,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdAuthorsDelete(id: string, body?: RemoveAuthorFromBookDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdAuthorsDelete(id: string, body?: RemoveAuthorFromBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdAuthorsDelete(id: string, body?: RemoveAuthorFromBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdAuthorsDelete(id: string, body?: RemoveAuthorFromBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdAuthorsDelete.');
    }


    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<any>('delete', `${this.basePath}/books/${encodeURIComponent(String(id))}/authors`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdAuthorsPut(id: number, body?: AddAuthorToBookDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdAuthorsPut(id: number, body?: AddAuthorToBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdAuthorsPut(id: number, body?: AddAuthorToBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdAuthorsPut(id: number, body?: AddAuthorToBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdAuthorsPut.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];


    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<any>('put', `${this.basePath}/books/${encodeURIComponent(String(id))}/authors`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdDelete(id: number, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdDelete(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdDelete(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdDelete(id: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdDelete.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<any>('delete', `${this.basePath}/books/${encodeURIComponent(String(id))}`,
      {
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdGet(id: number, observe?: 'body', reportProgress?: boolean): Observable<BookWithReviewsResponseDto>;
  public apiBooksIdGet(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<BookWithReviewsResponseDto>>;
  public apiBooksIdGet(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<BookWithReviewsResponseDto>>;
  public apiBooksIdGet(id: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdGet.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<BookResponseDto>('get', `${this.basePath}/books/${encodeURIComponent(String(id))}`,
      {
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdLanguagesDelete(id: string, body?: RemoveLanguageFromBookDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdLanguagesDelete(id: string, body?: RemoveLanguageFromBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdLanguagesDelete(id: string, body?: RemoveLanguageFromBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdLanguagesDelete(id: string, body?: RemoveLanguageFromBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdLanguagesDelete.');
    }


    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<any>('delete', `${this.basePath}/books/${encodeURIComponent(String(id))}/languages`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdLanguagesPut(id: number, body?: AddLanguageToBookDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdLanguagesPut(id: number, body?: AddLanguageToBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdLanguagesPut(id: number, body?: AddLanguageToBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdLanguagesPut(id: number, body?: AddLanguageToBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdLanguagesPut.');
    }


    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<any>('put', `${this.basePath}/books/${encodeURIComponent(String(id))}/languages`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdPut(id: string, body?: UpdateBookDto, observe?: 'body', reportProgress?: boolean): Observable<BookResponseDto>;
  public apiBooksIdPut(id: string, body?: UpdateBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<BookResponseDto>>;
  public apiBooksIdPut(id: string, body?: UpdateBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<BookResponseDto>>;
  public apiBooksIdPut(id: string, body?: UpdateBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdPut.');
    }


    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<BookResponseDto>('put', `${this.basePath}/books/${encodeURIComponent(String(id))}`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdSubjectsDelete(id: string, body?: RemoveSubjectFromBookDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdSubjectsDelete(id: string, body?: RemoveSubjectFromBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdSubjectsDelete(id: string, body?: RemoveSubjectFromBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdSubjectsDelete(id: string, body?: RemoveSubjectFromBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdSubjectsDelete.');
    }


    let headers = this.defaultHeaders;


    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<any>('delete', `${this.basePath}/books/${encodeURIComponent(String(id))}/subjects`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param id
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksIdSubjectsPut(id: number, body?: AddSubjectToBookDto, observe?: 'body', reportProgress?: boolean): Observable<any>;
  public apiBooksIdSubjectsPut(id: number, body?: AddSubjectToBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
  public apiBooksIdSubjectsPut(id: number, body?: AddSubjectToBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
  public apiBooksIdSubjectsPut(id: number, body?: AddSubjectToBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (id === null || id === undefined) {
      throw new Error('Required parameter id was null or undefined when calling apiBooksIdSubjectsPut.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<any>('put', `${this.basePath}/books/${encodeURIComponent(String(id))}/subjects`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param key
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksKeyGet(key: string, observe?: 'body', reportProgress?: boolean): Observable<BookResponseDto>;
  public apiBooksKeyGet(key: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<BookResponseDto>>;
  public apiBooksKeyGet(key: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<BookResponseDto>>;
  public apiBooksKeyGet(key: string, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (key === null || key === undefined) {
      throw new Error('Required parameter key was null or undefined when calling apiBooksKeyGet.');
    }

    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<BookResponseDto>('get', `${this.basePath}/books/${encodeURIComponent(String(key))}`,
      {
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiBooksPost(body?: CreateBookDto, observe?: 'body', reportProgress?: boolean): Observable<BookResponseDto>;
  public apiBooksPost(body?: CreateBookDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<BookResponseDto>>;
  public apiBooksPost(body?: CreateBookDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<BookResponseDto>>;
  public apiBooksPost(body?: CreateBookDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {


    let headers = this.defaultHeaders;

    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];

    return this.httpClient.request<BookResponseDto>('post', `${this.basePath}/books`,
      {
        body: body,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  public uploadFile(id: number, file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.httpClient.post(
      `https://localhost:5000/books/${id}/image`,
      formData);
  }
}
