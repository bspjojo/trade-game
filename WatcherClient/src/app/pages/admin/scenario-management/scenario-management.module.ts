import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule, MatDialogModule, MatTableModule } from '@angular/material';

import { GameCreationDialogComponent } from './game-creation-dialog/game-creation-dialog.component';
import { ScenarioManagementComponent } from './scenario-management.component';
import { ScenarioService } from './scenario.service';

@NgModule({
    declarations: [
        ScenarioManagementComponent,
        GameCreationDialogComponent
    ],
    entryComponents: [
        GameCreationDialogComponent
    ],
    exports: [
        ScenarioManagementComponent
    ],
    providers: [
        ScenarioService
    ],
    imports: [
        CommonModule,
        MatButtonModule,
        MatTableModule,
        MatDialogModule
    ]
})
export class ScenarioManagementModule { }
