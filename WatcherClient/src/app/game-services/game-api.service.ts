import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Game } from './game.model';
import { GameDataService } from './game-data.service';
import { ConfigService } from '../app-config/config.service';

@Injectable()
export class GameApiService {

    constructor(private configService: ConfigService, private httpClient: HttpClient, private gameDataService: GameDataService) { }

    public async setGameScores(gameId: string): Promise<void> {
        let game = await this.httpClient.get<Game>(`${this.configService.config.apiUrl}api/game/scores/${gameId}`).toPromise();
        this.gameDataService.setNewGameData(game);
    }
}
