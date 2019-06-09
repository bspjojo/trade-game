import { Component, OnInit } from '@angular/core';

import { ScenarioSummary } from './scenario-summary';
import { ScenarioService } from './scenario.service';

@Component({
    selector: 'watcher-scenario-management',
    templateUrl: './scenario-management.component.html',
    styleUrls: ['./scenario-management.component.less']
})
export class ScenarioManagementComponent implements OnInit {
    public scenarios: ScenarioSummary[];
    public displayedColumns: string[];

    constructor(private scenarioService: ScenarioService) { }

    public ngOnInit(): void {
        this.displayedColumns = ['name', 'duration', 'weight', 'create'];

        this.reloadScenarios();
    }

    public async reloadScenarios(): Promise<void> {
        this.scenarios = await this.scenarioService.getScenarios();
    }

    public createGame(scenario: ScenarioSummary): void {
        console.log(scenario);
    }
}
