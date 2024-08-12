import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {reviewEndpoints} from "../constants/endpoints";
import {CreateReviewModel, Review} from "../models/review";
import * as http from "node:http";

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private httpClient: HttpClient) {
  }

  async post(review: CreateReviewModel) {
    return this.httpClient.post<Review>(reviewEndpoints.reviews, review);
  }


  async like(reviewId: string, userId: number) {
    return this.httpClient.put(reviewEndpoints.likes(reviewId), {
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

    return this.httpClient.delete(reviewEndpoints.likes(reviewId), options);
  }

  async dislike(reviewId: string, userId: number) {
    return this.httpClient.put(reviewEndpoints.dislikes(reviewId), {
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

    return this.httpClient.delete(reviewEndpoints.dislikes(reviewId), options);
  }
}
