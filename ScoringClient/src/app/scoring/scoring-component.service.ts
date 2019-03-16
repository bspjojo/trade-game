import { Injectable } from '@angular/core';
import { CountrySelection } from './country-selection/country-selection.model';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from '../app-config/config.service';
import { GameSelectionService } from '../game-selection/game-selection.service';

@Injectable()
export class ScoringComponentService {
    private selectedCountry: CountrySelection;

    constructor(private configService: ConfigService, private httpClient: HttpClient,
        private gameSelectionService: GameSelectionService) { }

    public set country(selectedCountry: CountrySelection) {
        this.selectedCountry = selectedCountry;
    }

    public get country(): CountrySelection {
        return this.selectedCountry;
    }

    public getCountries(gameId: string): Promise<CountrySelection[]> {
        return this.httpClient.get<CountrySelection[]>(this.configService.config.apiUrl + 'api/game/countries/' + gameId).toPromise();
    }

    public updateScore(yearResults): Promise<any> {
        let processedRequestObject = {
            gameId: this.gameSelectionService.game.id,
            countryId: this.selectedCountry.id,
            yearResults
        };

        return this.httpClient.post<any>(this.configService.config.apiUrl + 'api/game/updateScores', processedRequestObject).toPromise();
    }
}
