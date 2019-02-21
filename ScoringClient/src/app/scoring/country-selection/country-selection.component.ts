import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ScoringComponentService } from '../scoring-component.service';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs/Subject';
import { GameSelectionService } from '../../game-selection/game-selection.service';
import { CountrySelection } from './country-selection.model';

@Component({
    selector: 'score-country-selection',
    templateUrl: './country-selection.component.html',
    styleUrls: ['./country-selection.component.less']
})
export class CountrySelectionComponent implements OnInit, OnDestroy {
    public countries: CountrySelection[];
    public countrySelectionControl: FormControl;
    @Output() public country: EventEmitter<CountrySelection>;

    private ngUnsubscribe: Subject<void>;

    constructor(private scoringService: ScoringComponentService, private gameSelectionService: GameSelectionService) {
        this.ngUnsubscribe = new Subject();
        this.country = new EventEmitter<CountrySelection>();
    }

    public async ngOnInit(): Promise<void> {
        this.countrySelectionControl = new FormControl();
        this.countrySelectionControl.valueChanges.takeUntil(this.ngUnsubscribe).subscribe(selectedCountry => {
            this.country.emit(selectedCountry);
        });

        this.countries = [];
        this.countries = await this.scoringService.getCountries(this.gameSelectionService.game.id);
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
