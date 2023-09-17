import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CorporateCrewComponent } from './corporate-crew.component';

describe('CorporateCrewComponent', () => {
  let component: CorporateCrewComponent;
  let fixture: ComponentFixture<CorporateCrewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [CorporateCrewComponent]
    });
    fixture = TestBed.createComponent(CorporateCrewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
