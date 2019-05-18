import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { ScenarioEditorService } from './scenario-editor.service';
import { Scenario } from './scenario.model';

@Component({
    selector: 'watcher-scenario-editor',
    templateUrl: './scenario-editor.component.html',
    styleUrls: ['./scenario-editor.component.less', './editor-shared.component.less']
})
export class ScenarioEditorComponent implements OnInit, OnDestroy {
    public scenario: Scenario;
    public saving: boolean;
    public scenarioFormGroup: FormGroup;

    private ngUnsubscribe: Subject<void>;

    constructor(private scenarioEditorService: ScenarioEditorService) {
        this.ngUnsubscribe = new Subject();
    }

    public ngOnInit(): void {
        this.saving = false;
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public createScenario(): void {
        this.scenario = {
            countries: []
        };

        this.setupScenarioFormGroup();
    }

    public addCountry(): void {
        this.scenario.countries.push({} as any);
    }

    public async save(): Promise<void> {
        this.saving = true;

        this.scenario = await this.scenarioEditorService.save(this.scenario);
        this.setupScenarioFormGroup();

        this.saving = false;
    }

    private setupScenarioFormGroup(): void {
        this.ngUnsubscribe.next();

        let name = new FormControl(this.scenario.name, Validators.required);
        name.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe(v => this.scenario.name = v);

        let duration = new FormControl(this.scenario.duration, Validators.required);
        duration.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe(v => this.scenario.duration = v);

        let author = new FormControl(this.scenario.author, Validators.required);
        author.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe(v => this.scenario.author = v);

        this.scenarioFormGroup = new FormGroup({
            name,
            duration,
            author
        });
    }
}
