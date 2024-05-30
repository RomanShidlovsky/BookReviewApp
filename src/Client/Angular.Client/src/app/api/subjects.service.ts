import {Injectable} from '@angular/core';
import {HttpClient, HttpEvent, HttpHeaders, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {gateway} from "../constants/endpoints";
import {SubjectResponseDto} from "../models/subject/subjectResponseDto";
import {UpdateSubjectDto} from "../models/subject/updateSubjectDto";
import {CreateSubjectDto} from "../models/subject/createSubjectDto";

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

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
    public apiSubjectsGet(observe?: 'body', reportProgress?: boolean): Observable<Array<SubjectResponseDto>>;
    public apiSubjectsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<SubjectResponseDto>>>;
    public apiSubjectsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<SubjectResponseDto>>>;
    public apiSubjectsGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.request<Array<SubjectResponseDto>>('get',`${this.basePath}/subjects`,
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
    public apiSubjectsIdDelete(id: number, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiSubjectsIdDelete(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiSubjectsIdDelete(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiSubjectsIdDelete(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiSubjectsIdDelete.');
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

        return this.httpClient.request<any>('delete',`${this.basePath}/subjects/${encodeURIComponent(String(id))}`,
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
    public apiSubjectsIdGet(id: number, observe?: 'body', reportProgress?: boolean): Observable<SubjectResponseDto>;
    public apiSubjectsIdGet(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<SubjectResponseDto>>;
    public apiSubjectsIdGet(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<SubjectResponseDto>>;
    public apiSubjectsIdGet(id: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiSubjectsIdGet.');
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

        return this.httpClient.request<SubjectResponseDto>('get',`${this.basePath}/subjects/${encodeURIComponent(String(id))}`,
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
    public apiSubjectsIdPut(id: number, body?: UpdateSubjectDto, observe?: 'body', reportProgress?: boolean): Observable<SubjectResponseDto>;
    public apiSubjectsIdPut(id: number, body?: UpdateSubjectDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<SubjectResponseDto>>;
    public apiSubjectsIdPut(id: number, body?: UpdateSubjectDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<SubjectResponseDto>>;
    public apiSubjectsIdPut(id: number, body?: UpdateSubjectDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiSubjectsIdPut.');
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

        return this.httpClient.request<SubjectResponseDto>('put',`${this.basePath}/subjects/${encodeURIComponent(String(id))}`,
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
    public apiSubjectsPost(body?: CreateSubjectDto, observe?: 'body', reportProgress?: boolean): Observable<SubjectResponseDto>;
    public apiSubjectsPost(body?: CreateSubjectDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<SubjectResponseDto>>;
    public apiSubjectsPost(body?: CreateSubjectDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<SubjectResponseDto>>;
    public apiSubjectsPost(body?: CreateSubjectDto, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


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

        return this.httpClient.request<SubjectResponseDto>('post',`${this.basePath}/subjects`,
            {
                body: body,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
