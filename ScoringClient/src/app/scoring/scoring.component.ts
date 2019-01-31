import { Component, OnInit } from '@angular/core';
import { GameSelectionService } from '../game-selection/game-selection.service';
import { CountrySelection } from './country-selection/country-selection.model';

@Component({
    selector: 'score-scoring',
    templateUrl: './scoring.component.html',
    styleUrls: ['./scoring.component.less']
})
export class ScoringComponent implements OnInit {

    constructor(private gameSelectionService: GameSelectionService) { }

    public ngOnInit(): void {
    }

    public countrySelected(countrySelection: CountrySelection): void {
        console.log(countrySelection);
    }

    public get show(): boolean {
        return this.gameSelectionService.game != null;
    }
}
