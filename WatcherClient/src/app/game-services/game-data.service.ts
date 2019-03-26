import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Game } from './game.model';

@Injectable({
    providedIn: 'root'
})
export class GameDataService {
    private gameBehaviourSubject: BehaviorSubject<Game>;

    constructor() {
        this.gameBehaviourSubject = new BehaviorSubject<Game>(null);
    }

    public setNewGameData(gameData: Game): void {
        console.log('Game data updated', gameData);

        this.gameBehaviourSubject.next(gameData);
    }

    public get gameSubject(): Observable<Game> {
        return this.gameBehaviourSubject;
    }
}
