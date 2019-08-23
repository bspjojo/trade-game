import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../app-config/config.service';
import { GameSelection } from './game-selection.model';

@Injectable()
export class GameSelectionService {
    private selectedGame: GameSelection;

    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public async getGames(): Promise<GameSelection[]> {
        let config = await this.configService.getConfig();

        return this.httpClient.get<GameSelection[]>(config.apiUrl + 'api/game/games').toPromise();
    }

    public set game(selectedGame: GameSelection) {
        this.selectedGame = selectedGame;
    }

    public get game(): GameSelection {
        return this.selectedGame;
    }
}
