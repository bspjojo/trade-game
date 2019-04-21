import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { GameApiService } from 'src/app/game-services/game-api.service';
import { GameDataService } from 'src/app/game-services/game-data.service';
import { GameHubService } from 'src/app/game-services/game-hub.service';
import { Game } from 'src/app/game-services/game.model';

import { GameSelection } from './game-selection.model';
import { GameSelectionService } from './game-selection.service';

@Component({
    selector: 'watcher-join-game',
    templateUrl: './join-game.component.html',
    styleUrls: ['./join-game.component.less']
})
export class JoinGameComponent implements OnInit, OnDestroy {
    public games: GameSelection[];
    public gameSelectionControl: FormControl;
    public gameYearControl: FormControl;
    private gameChangeSubject: Subject<void>;
    public game: Game;

    private ngUnsubscribe: Subject<void>;

    constructor(private gameSelectionService: GameSelectionService, private gameService: GameHubService,
        private gameDataService: GameDataService, private gameApiService: GameApiService) {
        this.ngUnsubscribe = new Subject();
    }

    public async ngOnInit(): Promise<void> {
        this.gameChangeSubject = new Subject();
        let game = this.gameSelectionService.game;

        let gameId = game != null ? game.id : null;
        this.gameSelectionControl = new FormControl(gameId);
        this.gameSelectionControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((selectedGameId: string) => {
            let pGame = this.gameSelectionService.game;
            let selectedGame = this.games.find(v => v.id === selectedGameId);
            this.gameSelectionService.game = selectedGame;

            if (pGame != null) {
                this.gameService.leaveGame(pGame.id);
            }

            if (selectedGameId != null) {
                this.gameService.joinGame(selectedGameId);
            }

            this.gameApiService.getGameScores(selectedGameId);
        });

        this.gameDataService.gameSubject.pipe(takeUntil(this.ngUnsubscribe)).subscribe((gameData: Game) => {
            this.game = gameData;
            this.gameYearControl = new FormControl(this.game != null ? this.game.currentYear : null);

            this.gameChangeSubject.next();

            this.gameYearControl.valueChanges.pipe(takeUntil(this.gameChangeSubject)).subscribe((year: number) => {
                this.gameApiService.setGameYear(this.game.id, year);
            });
        });

        this.games = [];
        this.games = await this.gameSelectionService.getGames();
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
