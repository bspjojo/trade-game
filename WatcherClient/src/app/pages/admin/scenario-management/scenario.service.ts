import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from 'src/app/app-config/config.service';
import { ScenarioSummary } from './scenario-summary';

@Injectable()
export class ScenarioService {
    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public async getScenarios(): Promise<ScenarioSummary[]> {
        let config = await this.configService.getConfig();

        return this.httpClient.get<ScenarioSummary[]>(config.apiUrl + 'api/scenario/list').toPromise();
    }

    public async createGameFromScenario(scenarioId: string, gameName: string): Promise<string> {
        console.log(scenarioId);
        let config = await this.configService.getConfig();
        return this.httpClient.post<string>(config.apiUrl + 'api/game/createFromScenario',
            { scenarioId, name: gameName }).toPromise();
    }
}
