import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from 'src/app/app-config/config.service';
import { Scenario } from './scenario.model';

@Injectable()
export class ScenarioEditorService {

    constructor(private configService: ConfigService, private httpClient: HttpClient) { }

    public async save(scenario: Scenario): Promise<Scenario> {
        let config = await this.configService.getConfig();
        return this.httpClient.post<Scenario>(config.apiUrl + 'api/scenario/update', scenario).toPromise();
    }
}
