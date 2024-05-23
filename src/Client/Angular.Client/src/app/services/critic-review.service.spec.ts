import { TestBed } from '@angular/core/testing';

import { CriticReviewService } from './critic-review.service';

describe('CriticReviewService', () => {
  let service: CriticReviewService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CriticReviewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
