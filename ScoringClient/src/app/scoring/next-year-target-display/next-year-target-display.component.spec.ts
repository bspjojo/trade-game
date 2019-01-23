import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextYearTargetDisplayComponent } from './next-year-target-display.component';

describe('NextYearTargetDisplayComponent', () => {
    let component: NextYearTargetDisplayComponent;
    let fixture: ComponentFixture<NextYearTargetDisplayComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [NextYearTargetDisplayComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(NextYearTargetDisplayComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
