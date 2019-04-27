import { Component } from '@angular/core';

import { Scenario } from './scenario.model';

@Component({
    selector: 'watcher-scenario-editor',
    templateUrl: './scenario-editor.component.html',
    styleUrls: ['./scenario-editor.component.less', './editor-shared.component.less']
})
export class ScenarioEditorComponent {
    public scenario: Scenario;

    public createScenario(): void {
        this.scenario = {
            countries: []
        };
    }

    public addCountry(): void {
        this.scenario.countries.push({} as any);
    }
}
