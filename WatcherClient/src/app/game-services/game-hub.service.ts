import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

import { ConfigService } from '../app-config/config.service';
import { GameDataService } from './game-data.service';
import { Game } from './game.model';

@Injectable()
export class GameHubService {
    private hubConnection: signalR.HubConnection;
    private hubConnectionStartPromise: Promise<void>;

    constructor(configService: ConfigService, gameDataService: GameDataService) {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(configService.config.apiUrl + 'hubs/game')
            .build();

        this.hubConnectionStartPromise = this.hubConnection.start();

        this.hubConnectionStartPromise
            .then(() => {
                console.log('Connection started!');

                this.hubConnection.on('ScoresUpdated', (gameData: Game) => gameDataService.setNewGameData(gameData));
            })
            .catch(err => console.log('Error while establishing connection', err));
    }

    public joinGame(gameId: string): void {
        console.log('joining game', gameId);
        this.hubConnection.send('joinGame', gameId);
    }

    public leaveGame(gameId: string): void {
        console.log('leaving game', gameId);
        this.hubConnection.send('leaveGame', gameId);
    }
}
