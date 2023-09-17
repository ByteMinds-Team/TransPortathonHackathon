import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CorporateVehicleComponent } from './corporate-vehicle.component';

describe('CorporateVehicleComponent', () => {
  let component: CorporateVehicleComponent;
  let fixture: ComponentFixture<CorporateVehicleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [CorporateVehicleComponent]
    });
    fixture = TestBed.createComponent(CorporateVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
