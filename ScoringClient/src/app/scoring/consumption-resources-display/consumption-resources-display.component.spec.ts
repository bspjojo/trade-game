import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsumptionResourcesDisplayComponent } from './consumption-resources-display.component';

describe('ConsumptionResourcesDisplayComponent', () => {
  let component: ConsumptionResourcesDisplayComponent;
  let fixture: ComponentFixture<ConsumptionResourcesDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsumptionResourcesDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsumptionResourcesDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
