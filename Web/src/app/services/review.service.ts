import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IReview } from '../models/IReview';

@Injectable({
  providedIn: 'root'
})


export class ReviewService {

  constructor(private httpClient: HttpClient) { }
  reviewUrl: string = environment.reviewUrl;

  getAllPReviews(): Observable<IReview> {
    return this.httpClient.get<IReview>(
      (this.reviewUrl + "/Review")
    );
  }
  
  createReview(product : IReview): Observable<IReview> {
    return this.httpClient.post<IReview>(
      this.reviewUrl + "/Review", product);
  }
}
