import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTransportRequestModalComponent } from './create-transport-request-modal.component';

describe('CreateTransportRequestModalComponent', () => {
  let component: CreateTransportRequestModalComponent;
  let fixture: ComponentFixture<CreateTransportRequestModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [CreateTransportRequestModalComponent]
    });
    fixture = TestBed.createComponent(CreateTransportRequestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
