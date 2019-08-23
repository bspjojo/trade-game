import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../app-config/config.service';
import { GameSelectionService } from '../game-selection/game-selection.service';
import { CountrySelection } from './country-selection/country-selection.model';

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

    public async getCountries(gameId: string): Promise<CountrySelection[]> {
        let config = await this.configService.getConfig();

        return this.httpClient.get<CountrySelection[]>(config.apiUrl + 'api/game/countries/' + gameId).toPromise();
    }

    public async updateScore(yearResults): Promise<any> {
        let processedRequestObject = {
            gameId: this.gameSelectionService.game.id,
            countryId: this.selectedCountry.id,
            yearResults
        };

        let config = await this.configService.getConfig();

        return this.httpClient.post<any>(config.apiUrl + 'api/game/updateScores', processedRequestObject).toPromise();
    }
}
