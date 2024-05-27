import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookLineComponent } from './book-line.component';

describe('BookLineComponent', () => {
  let component: BookLineComponent;
  let fixture: ComponentFixture<BookLineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookLineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BookLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
