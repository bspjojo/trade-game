import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatTabsModule } from '@angular/material';

import { AdminComponent } from './admin.component';
import { GameManagementModule } from './game-management/game-management.module';
import { ScenarioEditorModule } from './scenario-editor/scenario-editor.module';

@NgModule({
    declarations: [AdminComponent],
    exports: [AdminComponent],
    imports: [
        MatTabsModule,
        ScenarioEditorModule,
        GameManagementModule,
        CommonModule
    ]
})
export class AdminModule { }
