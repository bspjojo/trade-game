import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule, MatTableModule } from '@angular/material';

import { ScenarioManagementComponent } from './scenario-management.component';
import { ScenarioService } from './scenario.service';

@NgModule({
    declarations: [ScenarioManagementComponent],
    exports: [ScenarioManagementComponent],
    providers: [ScenarioService],
    imports: [
        CommonModule,
        MatButtonModule,
        MatTableModule
    ]
})
export class ScenarioManagementModule { }
