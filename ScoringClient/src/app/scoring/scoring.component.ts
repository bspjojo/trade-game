import { Component, OnInit } from '@angular/core';
import { GameSelectionService } from '../game-selection/game-selection.service';
import { CountrySelection } from './country-selection/country-selection.model';
import { ScoringComponentService } from './scoring-component.service';

@Component({
    selector: 'score-scoring',
    templateUrl: './scoring.component.html',
    styleUrls: ['./scoring.component.less']
})
export class ScoringComponent implements OnInit {
    private selectedCountry: CountrySelection;
    private apiResponse: any;

    constructor(private gameSelectionService: GameSelectionService, private scoringComponentService: ScoringComponentService) { }

    public ngOnInit(): void {
    }

    public countrySelected(countrySelection: CountrySelection): void {
        this.selectedCountry = countrySelection;
        this.scoringComponentService.country = countrySelection;
        this.apiResponse = null;
    }

    public get show(): boolean {
        return this.gameSelectionService.game != null;
    }

    public get showResultEntry(): boolean {
        return this.selectedCountry != null;
    }
}
