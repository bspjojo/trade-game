import { Component, OnInit } from '@angular/core';

import { ScenarioSummary } from './scenario-summary';
import { ScenarioService } from './scenario.service';

@Component({
    selector: 'watcher-game-management',
    templateUrl: './game-management.component.html',
    styleUrls: ['./game-management.component.less']
})
export class GameManagementComponent implements OnInit {
    public scenarios: ScenarioSummary[];

    constructor(private scenarioService: ScenarioService) { }

    public ngOnInit(): void {
        this.reloadScenarios();
    }

    public async reloadScenarios(): Promise<void> {
        this.scenarios = await this.scenarioService.getScenarios();
    }

    public createGame(scenario: ScenarioSummary): void {
        console.log(scenario);
    }
}
