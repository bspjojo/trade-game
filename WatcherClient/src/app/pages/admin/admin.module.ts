import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatTabsModule } from '@angular/material';

import { AdminComponent } from './admin.component';
import { ScenarioEditorModule } from './scenario-editor/scenario-editor.module';
import { ScenarioManagementModule } from './scenario-management/scenario-management.module';

@NgModule({
    declarations: [AdminComponent],
    exports: [AdminComponent],
    imports: [
        MatTabsModule,
        ScenarioEditorModule,
        ScenarioManagementModule,
        CommonModule
    ]
})
export class AdminModule { }
