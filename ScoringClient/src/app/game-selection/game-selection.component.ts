import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { GameSelectionService } from './game-selection.service';
import { GameSelection } from './game-selection.model';
import { Subject } from 'rxjs/Subject';

@Component({
    selector: 'score-game-selection',
    templateUrl: './game-selection.component.html',
    styleUrls: ['./game-selection.component.less']
})
export class GameSelectionComponent implements OnInit {
    public games: GameSelection[];
    public gameSelectionControl: FormControl;

    private ngUnsubscribe: Subject<void>;

    constructor(private gameSelectionService: GameSelectionService) {
        this.ngUnsubscribe = new Subject();
    }

    public async ngOnInit(): Promise<void> {
        this.gameSelectionControl = new FormControl(this.gameSelectionService.game);
        this.gameSelectionControl.valueChanges.takeUntil(this.ngUnsubscribe).subscribe(selectedGame => {
            this.gameSelectionService.game = selectedGame;
        });
        this.games = [];
        this.games = await this.gameSelectionService.getCountries();
    }
}
