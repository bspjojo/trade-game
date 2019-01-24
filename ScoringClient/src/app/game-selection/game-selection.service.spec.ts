import { TestBed, inject } from '@angular/core/testing';

import { GameSelectionService } from './game-selection.service';

describe('GameSelectionService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [GameSelectionService]
        });
    });

    it('should be created', inject([GameSelectionService], (service: GameSelectionService) => {
        expect(service).toBeTruthy();
    }));
});
