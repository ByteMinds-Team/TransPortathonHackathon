import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyOfferComponent } from './my-offer.component';

describe('MyOfferComponent', () => {
  let component: MyOfferComponent;
  let fixture: ComponentFixture<MyOfferComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [MyOfferComponent]
    });
    fixture = TestBed.createComponent(MyOfferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
