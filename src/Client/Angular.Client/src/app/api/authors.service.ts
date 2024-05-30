import {Injectable} from '@angular/core';
import {HttpClient, HttpEvent, HttpHeaders, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthorResponseDto} from "../models/author/authorResponseDto";
import {UpdateAuthorDto} from "../models/author/updateAuthorDto";
import {CreateAuthorDto} from "../models/author/createAuthorDto";
import {gateway} from "../constants/endpoints";


@Injectable({
  providedIn: 'root'
})
export class AuthorsService {

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
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiAuthorsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<AuthorResponseDto>>;
    public apiAuthorsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<AuthorResponseDto>>>;
    public apiAuthorsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<AuthorResponseDto>>>;
    public apiAuthorsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<AuthorResponseDto>>('get',`${this.basePath}/authors`,
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
    public apiAuthorsIdDelete(id: number, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiAuthorsIdDelete(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiAuthorsIdDelete(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiAuthorsIdDelete(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiAuthorsIdDelete.');
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
        ];

        return this.httpClient.request<any>('delete',`${this.basePath}/authors/${encodeURIComponent(String(id))}`,
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
    public apiAuthorsIdGet(id: number, observe?: 'body', reportProgress?: boolean): Observable<AuthorResponseDto>;
    public apiAuthorsIdGet(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AuthorResponseDto>>;
    public apiAuthorsIdGet(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AuthorResponseDto>>;
    public apiAuthorsIdGet(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiAuthorsIdGet.');
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
        ];

        return this.httpClient.request<AuthorResponseDto>('get',`${this.basePath}/authors/${encodeURIComponent(String(id))}`,
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
    public apiAuthorsIdPut(id: number, body?: UpdateAuthorDto, observe?: 'body', reportProgress?: boolean): Observable<AuthorResponseDto>;
    public apiAuthorsIdPut(id: number, body?: UpdateAuthorDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AuthorResponseDto>>;
    public apiAuthorsIdPut(id: number, body?: UpdateAuthorDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AuthorResponseDto>>;
    public apiAuthorsIdPut(id: number, body?: UpdateAuthorDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiAuthorsIdPut.');
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

        return this.httpClient.request<AuthorResponseDto>('put',`${this.basePath}/authors/${encodeURIComponent(String(id))}`,
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
    public apiAuthorsKeyGet(key: string, observe?: 'body', reportProgress?: boolean): Observable<AuthorResponseDto>;
    public apiAuthorsKeyGet(key: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AuthorResponseDto>>;
    public apiAuthorsKeyGet(key: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AuthorResponseDto>>;
    public apiAuthorsKeyGet(key: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (key === null || key === undefined) {
            throw new Error('Required parameter key was null or undefined when calling apiAuthorsKeyGet.');
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
        ];

        return this.httpClient.request<AuthorResponseDto>('get',`${this.basePath}/authors/${encodeURIComponent(String(key))}`,
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
    public apiAuthorsPost(body?: CreateAuthorDto, observe?: 'body', reportProgress?: boolean): Observable<AuthorResponseDto>;
    public apiAuthorsPost(body?: CreateAuthorDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AuthorResponseDto>>;
    public apiAuthorsPost(body?: CreateAuthorDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AuthorResponseDto>>;
    public apiAuthorsPost(body?: CreateAuthorDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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

        return this.httpClient.request<AuthorResponseDto>('post',`${this.basePath}/authors`,
            {
                body: body,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    public uploadFile (id: number, file : File)
    {
      const formData = new FormData();
      formData.append('file', file, file.name);

      return this.httpClient.post(
        `https://localhost:5000/authors/${id}/image`,
        formData);
    }
}
