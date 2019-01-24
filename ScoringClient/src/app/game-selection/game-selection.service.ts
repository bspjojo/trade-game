import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ConfigService } from '../app-config/config.service';
import { GameSelection } from './game-selection.model';

@Injectable()
export class GameSelectionService {
    private selectedGame: GameSelection;

    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public getCountries(): Promise<GameSelection[]> {
        return this.httpClient.get<GameSelection[]>(this.configService.config.apiUrl + 'api/game/games').toPromise();
    }

    public setGame(selectedGame: GameSelection): void {
        this.selectedGame = selectedGame;
    }

    public getGame(): GameSelection {
        return this.selectedGame;
    }
}
