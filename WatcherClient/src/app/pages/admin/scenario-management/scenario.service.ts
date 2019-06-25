import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from 'src/app/app-config/config.service';

import { ScenarioSummary } from './scenario-summary';

@Injectable()
export class ScenarioService {
    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public getScenarios(): Promise<ScenarioSummary[]> {
        return this.httpClient.get<ScenarioSummary[]>(this.configService.config.apiUrl + 'api/scenario/list').toPromise();
    }

    public createGameFromScenario(scenarioId: string, gameName: string): Promise<string> {
        console.log(scenarioId);
        return this.httpClient.post<string>(this.configService.config.apiUrl + 'api/game/createFromScenario',
            { scenarioId, name: gameName }).toPromise();
    }
}
