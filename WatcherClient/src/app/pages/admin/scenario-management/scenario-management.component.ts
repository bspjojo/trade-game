import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';

import { GameCreationDialogComponent } from './game-creation-dialog/game-creation-dialog.component';
import { GameCreationData, ScenarioSummary } from './scenario-summary';
import { ScenarioService } from './scenario.service';

@Component({
    selector: 'watcher-scenario-management',
    templateUrl: './scenario-management.component.html',
    styleUrls: ['./scenario-management.component.less']
})
export class ScenarioManagementComponent implements OnInit {
    public scenarios: ScenarioSummary[];
    public displayedColumns: string[];

    constructor(private scenarioService: ScenarioService, private dialog: MatDialog) { }

    public ngOnInit(): void {
        this.displayedColumns = ['name', 'duration', 'weight', 'create'];

        this.reloadScenarios();
    }

    public async reloadScenarios(): Promise<void> {
        this.scenarios = await this.scenarioService.getScenarios();
    }

    public createGame(scenario: ScenarioSummary): void {
        const dialogRef = this.dialog.open(GameCreationDialogComponent, {
            width: '400px',
            data: scenario
        });

        dialogRef.afterClosed().subscribe((result: GameCreationData) => {
            if (result != null) {
                this.scenarioService.createGameFromScenario(result.scenarioId, result.name);
            }

            console.log('The dialog was closed');
        });
    }
}
