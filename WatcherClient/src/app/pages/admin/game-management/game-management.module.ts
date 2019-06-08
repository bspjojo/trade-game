import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GameManagementComponent } from './game-management.component';
import { ListScenarioService } from './list-scenario.service';

@NgModule({
    declarations: [GameManagementComponent],
    exports: [GameManagementComponent],
    providers: [ListScenarioService],
    imports: [
        CommonModule
    ]
})
export class GameManagementModule { }
