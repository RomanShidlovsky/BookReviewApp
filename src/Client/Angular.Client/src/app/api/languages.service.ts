import {Injectable} from '@angular/core';
import {HttpClient, HttpEvent, HttpHeaders, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {LanguageResponseDto} from "../models/language/languageResponseDto";
import {UpdateLanguageDto} from "../models/language/updateLanguageDto";
import {CreateLanguageDto} from "../models/language/createLanguageDto";
import {gateway} from "../constants/endpoints";

@Injectable({
  providedIn: 'root'
})
export class LanguagesService {

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
    public apiLanguagesGet(observe?: 'body', reportProgress?: boolean): Observable<Array<LanguageResponseDto>>;
    public apiLanguagesGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<LanguageResponseDto>>>;
    public apiLanguagesGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<LanguageResponseDto>>>;
    public apiLanguagesGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.request<Array<LanguageResponseDto>>('get',`${this.basePath}/languages`,
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
    public apiLanguagesIdDelete(id: number, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiLanguagesIdDelete(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiLanguagesIdDelete(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiLanguagesIdDelete(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiLanguagesIdDelete.');
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

        return this.httpClient.request<any>('delete',`${this.basePath}/languages/${encodeURIComponent(String(id))}`,
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
    public apiLanguagesIdGet(id: number, observe?: 'body', reportProgress?: boolean): Observable<LanguageResponseDto>;
    public apiLanguagesIdGet(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LanguageResponseDto>>;
    public apiLanguagesIdGet(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LanguageResponseDto>>;
    public apiLanguagesIdGet(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiLanguagesIdGet.');
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

        return this.httpClient.request<LanguageResponseDto>('get',`${this.basePath}/languages/${encodeURIComponent(String(id))}`,
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
    public apiLanguagesIdPut(id: string, body?: UpdateLanguageDto, observe?: 'body', reportProgress?: boolean): Observable<LanguageResponseDto>;
    public apiLanguagesIdPut(id: string, body?: UpdateLanguageDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LanguageResponseDto>>;
    public apiLanguagesIdPut(id: string, body?: UpdateLanguageDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LanguageResponseDto>>;
    public apiLanguagesIdPut(id: string, body?: UpdateLanguageDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiLanguagesIdPut.');
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

        return this.httpClient.request<LanguageResponseDto>('put',`${this.basePath}/languages/${encodeURIComponent(String(id))}`,
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
     * @param body
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiLanguagesPost(body?: CreateLanguageDto, observe?: 'body', reportProgress?: boolean): Observable<LanguageResponseDto>;
    public apiLanguagesPost(body?: CreateLanguageDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LanguageResponseDto>>;
    public apiLanguagesPost(body?: CreateLanguageDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LanguageResponseDto>>;
    public apiLanguagesPost(body?: CreateLanguageDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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

        return this.httpClient.request<LanguageResponseDto>('post',`${this.basePath}/languages`,
            {
                body: body,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
