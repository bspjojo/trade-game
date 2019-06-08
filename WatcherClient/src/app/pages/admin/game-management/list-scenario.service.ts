import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from 'src/app/app-config/config.service';

import { ScenarioSummary } from './scenario-summary';

@Injectable()
export class ListScenarioService {
    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public getScenarios(): Promise<ScenarioSummary[]> {
        return this.httpClient.get<ScenarioSummary[]>(this.configService.config.apiUrl + 'api/scenario/list').toPromise();
    }
}
