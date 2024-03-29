import { Component } from '@angular/core';
import { GameSelectionService } from '../game-selection/game-selection.service';
import { CountrySelection } from './country-selection/country-selection.model';
import { ScoringComponentService } from './scoring-component.service';
import { Subject } from 'rxjs';

@Component({
    selector: 'score-scoring',
    templateUrl: './scoring.component.html',
    styleUrls: ['./scoring.component.less']
})
export class ScoringComponent {
    private selectedCountry: CountrySelection;
    private apiResponse: any;
    public resetFormSubject: Subject<void>;

    constructor(private gameSelectionService: GameSelectionService, private scoringComponentService: ScoringComponentService) {
        this.resetFormSubject = new Subject();
    }

    public countrySelected(countrySelection: CountrySelection): void {
        this.selectedCountry = countrySelection;
        this.scoringComponentService.country = countrySelection;
        this.apiResponse = null;
        this.resetFormSubject.next();
    }

    public resultUpdated(res: any): void {
        this.apiResponse = res;
    }

    public get show(): boolean {
        return this.gameSelectionService.game != null;
    }

    public get showResultEntry(): boolean {
        return this.selectedCountry != null;
    }

    public get apiResponseNotNull(): boolean {
        return this.apiResponse != null;
    }
}
