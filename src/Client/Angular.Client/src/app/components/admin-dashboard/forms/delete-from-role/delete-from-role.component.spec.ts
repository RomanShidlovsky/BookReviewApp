import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteFromRoleComponent } from './delete-from-role.component';

describe('DeleteFromRoleComponent', () => {
  let component: DeleteFromRoleComponent;
  let fixture: ComponentFixture<DeleteFromRoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteFromRoleComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeleteFromRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
