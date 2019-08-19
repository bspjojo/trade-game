import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { GameCreationData, ScenarioSummary } from '../scenario-summary';

@Component({
    selector: 'watcher-game-creation-dialog',
    templateUrl: './game-creation-dialog.component.html',
    styleUrls: ['./game-creation-dialog.component.less']
})
export class GameCreationDialogComponent implements OnInit, OnDestroy {
    public nameControl: FormControl;
    public output: GameCreationData;

    private ngUnsubscribe: Subject<void>;

    constructor(
        public dialogRef: MatDialogRef<GameCreationDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: ScenarioSummary
    ) {
        this.ngUnsubscribe = new Subject<void>();
    }

    public ngOnInit(): void {
        this.output = {
            scenarioId: this.data.id,
            name: this.data.name + '-' + this.data.author
        };

        this.nameControl = new FormControl(this.output.name, Validators.required);
        this.nameControl.valueChanges.pipe(takeUntil(this.ngUnsubscribe)).subscribe((v: string) => {
            this.output.name = v;
        });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
