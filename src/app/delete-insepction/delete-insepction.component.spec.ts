import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteInsepctionComponent } from './delete-insepction.component';

describe('DeleteInsepctionComponent', () => {
  let component: DeleteInsepctionComponent;
  let fixture: ComponentFixture<DeleteInsepctionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteInsepctionComponent]
    });
    fixture = TestBed.createComponent(DeleteInsepctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
