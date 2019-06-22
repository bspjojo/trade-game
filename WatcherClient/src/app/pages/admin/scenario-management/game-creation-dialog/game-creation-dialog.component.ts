import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { ScenarioSummary } from '../scenario-summary';

@Component({
    selector: 'watcher-game-creation-dialog',
    templateUrl: './game-creation-dialog.component.html',
    styleUrls: ['./game-creation-dialog.component.less']
})
export class GameCreationDialogComponent implements OnInit {
    constructor(public dialogRef: MatDialogRef<GameCreationDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: ScenarioSummary) { }

    public ngOnInit(): void {
        // todo get scenario data.
    }
}
