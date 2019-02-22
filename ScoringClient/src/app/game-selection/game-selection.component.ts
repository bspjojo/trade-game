import { takeUntil } from 'rxjs/operators';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';

import { GameSelectionService } from './game-selection.service';
import { GameSelection } from './game-selection.model';
import { Subject } from 'rxjs';

@Component({
    selector: 'score-game-selection',
    templateUrl: './game-selection.component.html',
    styleUrls: ['./game-selection.component.less']
})
export class GameSelectionComponent implements OnInit, OnDestroy {
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
