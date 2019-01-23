import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExcessDisplayComponent } from './excess-display.component';

describe('ExcessDisplayComponent', () => {
    let component: ExcessDisplayComponent;
    let fixture: ComponentFixture<ExcessDisplayComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ExcessDisplayComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ExcessDisplayComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
