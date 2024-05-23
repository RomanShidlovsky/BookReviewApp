import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriticReviewSectionComponent } from './critic-review-section.component';

describe('CriticReviewSectionComponent', () => {
  let component: CriticReviewSectionComponent;
  let fixture: ComponentFixture<CriticReviewSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CriticReviewSectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CriticReviewSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
