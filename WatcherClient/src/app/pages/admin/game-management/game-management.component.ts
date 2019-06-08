import { Component, OnInit } from '@angular/core';

import { ListScenarioService } from './list-scenario.service';
import { ScenarioSummary } from './scenario-summary';

@Component({
    selector: 'watcher-game-management',
    templateUrl: './game-management.component.html',
    styleUrls: ['./game-management.component.less']
})
export class GameManagementComponent implements OnInit {
    public scenarios: ScenarioSummary[];

    constructor(private listScenarioService: ListScenarioService) { }

    public ngOnInit(): void {
        this.reloadScenarios();
    }

    public async reloadScenarios(): Promise<void> {
        this.scenarios = await this.listScenarioService.getScenarios();
    }

    public createGame(scenario: ScenarioSummary): void {
        console.log(scenario);
    }
}
