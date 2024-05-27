import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorLineComponent } from './author-line.component';

describe('AuthorLineComponent', () => {
  let component: AuthorLineComponent;
  let fixture: ComponentFixture<AuthorLineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthorLineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuthorLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
