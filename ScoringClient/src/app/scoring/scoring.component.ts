import { Component, OnInit } from '@angular/core';
import { GameSelectionService } from '../game-selection/game-selection.service';

@Component({
    selector: 'score-scoring',
    templateUrl: './scoring.component.html',
    styleUrls: ['./scoring.component.less']
})
export class ScoringComponent implements OnInit {

    constructor(private gameSelectionService: GameSelectionService) { }

    public ngOnInit(): void {
    }

    public get show(): boolean {
        return this.gameSelectionService.game != null;
    }
}
