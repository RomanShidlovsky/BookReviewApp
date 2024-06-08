import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddToRoleComponent } from './add-to-role.component';

describe('AddToRoleComponent', () => {
  let component: AddToRoleComponent;
  let fixture: ComponentFixture<AddToRoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddToRoleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddToRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
