import { TestBed, inject } from '@angular/core/testing';

import { GameSelectionService } from './game-selection.service';
import { ConfigService } from '../app-config/config.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

describe('GameSelectionService', () => {
    let service: GameSelectionService;

    beforeEach(() => {
        this.httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);

        this.mockConfigService = {
            config: {
                apiUrl: 'api/url/'
            }
        };

        this.httpClientSpyGetResponseContent = [{ data: 'value' }];
        this.httpClientSpy.get.and.callFake(() => Observable.of(this.httpClientSpyGetResponseContent));

        TestBed.configureTestingModule({
            providers: [
                GameSelectionService,
                { provide: ConfigService, useValue: this.mockConfigService },
                { provide: HttpClient, useValue: this.httpClientSpy }
            ]
        });

        service = TestBed.get(GameSelectionService);
    });

    describe('game', () => {
        it('should be able to get and set a value', () => {
            let val = { id: 'id', name: 'name' } as any;
            service.game = val;

            expect(service.game).toBe(val);
        });
    });

    describe('getGames', () => {
        it('should call to the configured api url /api/game/games', () => {
            service.getGames();

            expect(this.httpClientSpy.get).toHaveBeenCalledWith('api/url/api/game/games');
        });

        it('should return a promise that resolves to the result', async (done) => {
            let p = await service.getGames();

            expect(p).toBe(this.httpClientSpyGetResponseContent);

            done();
        });
    });
});
