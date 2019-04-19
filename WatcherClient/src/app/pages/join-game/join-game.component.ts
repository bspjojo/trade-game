import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { GameApiService } from 'src/app/game-services/game-api.service';
import { GameHubService } from 'src/app/game-services/game-hub.service';

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
    public selectedGame: GameSelection;

    private ngUnsubscribe: Subject<void>;

    constructor(private gameSelectionService: GameSelectionService, private gameService: GameHubService,
        private gameApiService: GameApiService) {
        this.ngUnsubscribe = new Subject();
    }

    public async ngOnInit(): Promise<void> {
        let game = this.gameSelectionService.game;
        this.selectedGame = game;

        let gameId = game != null ? game.id : null;
        this.gameSelectionControl = new FormControl(gameId);
        this.gameSelectionControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((selectedGame: string) => {
            let pGame = this.gameSelectionService.game;
            this.selectedGame = this.games.find(v => v.id === selectedGame);
            this.gameSelectionService.game = this.selectedGame;

            if (pGame != null) {
                this.gameService.leaveGame(pGame.id);
            }
            this.gameService.joinGame(selectedGame.id);
            this.gameApiService.setGameScores(selectedGame.id);
        });

        this.games = [];
        this.games = await this.gameSelectionService.getGames();
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
