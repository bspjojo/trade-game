import { TestBed, inject } from '@angular/core/testing';

import { ScoringComponentService } from './scoring-component.service';

describe('ScoringComponentService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [ScoringComponentService]
        });
    });

    it('should be created', inject([ScoringComponentService], (service: ScoringComponentService) => {
        expect(service).toBeTruthy();
    }));
});
