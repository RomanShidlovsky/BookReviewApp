import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {CreateReviewModel, Review} from "../models/review";
import {criticReviewEndpoints, reviewEndpoints} from "../constants/endpoints";

@Injectable({
  providedIn: 'root'
})
export class CriticReviewService {
  constructor(private httpClient: HttpClient) {
  }

  async post(review: CreateReviewModel) {
    return this.httpClient.post<Review>(criticReviewEndpoints.reviews, review);
  }


  async like(reviewId: string, userId: number) {
    return this.httpClient.put(criticReviewEndpoints.likes(reviewId), {
      reviewId,
      userId
    });
  }

  async unlike(reviewId: string, userId: number) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: {
        reviewId,
        userId
      },
    };

    return this.httpClient.delete(criticReviewEndpoints.likes(reviewId), options);
  }

  async dislike(reviewId: string, userId: number) {
    return this.httpClient.put(criticReviewEndpoints.dislikes(reviewId), {
      reviewId,
      userId
    });
  }

  async undislike(reviewId: string, userId: number) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: {
        reviewId,
        userId
      },
    };

    return this.httpClient.delete(criticReviewEndpoints.dislikes(reviewId), options);
  }
}
