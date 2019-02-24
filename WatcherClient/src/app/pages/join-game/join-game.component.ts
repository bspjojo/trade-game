import { takeUntil } from 'rxjs/operators';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { GameSelectionService } from './game-selection.service';
import { Subject } from 'rxjs';
import { GameSelection } from './game-selection.model';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'watcher-join-game',
    templateUrl: './join-game.component.html',
    styleUrls: ['./join-game.component.less']
})
export class JoinGameComponent implements OnInit, OnDestroy {
    public games: GameSelection[];
    public gameSelectionControl: FormControl;

    private ngUnsubscribe: Subject<void>;

    constructor(private gameSelectionService: GameSelectionService) {
        this.ngUnsubscribe = new Subject();
    }

    public async ngOnInit(): Promise<void> {
        this.gameSelectionControl = new FormControl(this.gameSelectionService.game);
        this.gameSelectionControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe(selectedGame => {
            this.gameSelectionService.game = selectedGame;
        });

        this.games = [];
        this.games = await this.gameSelectionService.getGames();
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
