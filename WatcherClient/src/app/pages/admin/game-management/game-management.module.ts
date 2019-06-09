import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule, MatTableModule } from '@angular/material';

import { GameManagementComponent } from './game-management.component';
import { ScenarioService } from './scenario.service';

@NgModule({
    declarations: [GameManagementComponent],
    exports: [GameManagementComponent],
    providers: [ScenarioService],
    imports: [
        CommonModule,
        MatButtonModule,
        MatTableModule
    ]
})
export class GameManagementModule { }
