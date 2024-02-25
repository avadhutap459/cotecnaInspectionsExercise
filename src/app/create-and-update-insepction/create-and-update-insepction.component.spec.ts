import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAndUpdateInsepctionComponent } from './create-and-update-insepction.component';

describe('CreateAndUpdateInsepctionComponent', () => {
  let component: CreateAndUpdateInsepctionComponent;
  let fixture: ComponentFixture<CreateAndUpdateInsepctionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateAndUpdateInsepctionComponent]
    });
    fixture = TestBed.createComponent(CreateAndUpdateInsepctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
