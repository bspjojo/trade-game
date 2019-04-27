import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { ScenarioCountry } from '../scenario.model';

@Component({
    selector: 'watcher-country-editor',
    templateUrl: './country-editor.component.html',
    styleUrls: ['./country-editor.component.less', './../editor-shared.component.less']
})
export class CountryEditorComponent implements OnInit, OnDestroy {
    @Input() public country: ScenarioCountry;
    private ngUnsubscribe: Subject<void>;

    public countryFormGroup: FormGroup;

    public ngOnInit(): void {
        this.ngUnsubscribe = new Subject();

        if (this.country.produce == null) {
            this.country.produce = {};
        }
        if (this.country.targets == null) {
            this.country.targets = {};
        }

        let fb = new FormBuilder();
        this.countryFormGroup = fb.group({});

        this.setupScenarioInformation(this.countryFormGroup);

        this.countryFormGroup.setControl('baselineProduce', this.setupBaselineProduce(fb));
        this.countryFormGroup.setControl('baselineTargets', this.setupBaselineTargets(fb));
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    private setupScenarioInformation(fg: FormGroup): void {
        let nameControl = new FormControl(this.country.name, [Validators.required]);
        nameControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: string) => {
            this.country.name = value;
        });
        fg.setControl('nameControl', nameControl);

        let targetScoreControl = new FormControl(this.country.targetScore, [Validators.required, Validators.min(0)]);
        targetScoreControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.targetScore = value;
        });
        fg.setControl('targetScoreControl', targetScoreControl);
    }

    private setupBaselineTargets(fb: FormBuilder): FormGroup {
        let chocolateControl = new FormControl(this.country.targets.chocolate, [Validators.required, Validators.min(0)]);
        chocolateControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.targets.chocolate = value;
        });

        let energyControl = new FormControl(this.country.targets.energy, [Validators.required, Validators.min(0)]);
        energyControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.targets.energy = value;
        });

        let grainControl = new FormControl(this.country.targets.grain, [Validators.required, Validators.min(0)]);
        grainControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.targets.grain = value;
        });

        let meatControl = new FormControl(this.country.targets.meat, [Validators.required, Validators.min(0)]);
        meatControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.targets.meat = value;
        });

        let textilesControl = new FormControl(this.country.targets.textiles, [Validators.required, Validators.min(0)]);
        textilesControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.targets.textiles = value;
        });

        return fb.group({
            chocolateControl,
            energyControl,
            grainControl,
            meatControl,
            textilesControl
        });
    }

    private setupBaselineProduce(fb: FormBuilder): FormGroup {
        let cocoaControl = new FormControl(this.country.produce.cocoa, [Validators.required, Validators.min(0)]);
        cocoaControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.produce.cocoa = value;
        });

        let cottonControl = new FormControl(this.country.produce.cotton, [Validators.required, Validators.min(0)]);
        cocoaControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.produce.cotton = value;
        });

        let grainControl = new FormControl(this.country.produce.grain, [Validators.required, Validators.min(0)]);
        grainControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.produce.grain = value;
        });

        let meatControl = new FormControl(this.country.produce.meat, [Validators.required, Validators.min(0)]);
        meatControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.produce.meat = value;
        });

        let oilControl = new FormControl(this.country.produce.oil, [Validators.required, Validators.min(0)]);
        oilControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((value: number) => {
            this.country.produce.oil = value;
        });

        return fb.group({
            cocoaControl,
            cottonControl,
            grainControl,
            meatControl,
            oilControl
        });
    }
}
