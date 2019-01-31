import { Injectable } from '@angular/core';
import { CountrySelection } from './country-selection/country-selection.model';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from '../app-config/config.service';

@Injectable()
export class ScoringComponentService {

    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public getCountries(gameId: string): Promise<CountrySelection[]> {
        return this.httpClient.get<CountrySelection[]>(this.configService.config.apiUrl + 'api/game/countries/' + gameId).toPromise();
    }
}
