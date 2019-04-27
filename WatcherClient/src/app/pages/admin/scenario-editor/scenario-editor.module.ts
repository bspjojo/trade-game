import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule, MatInputModule } from '@angular/material';

import { CountryEditorComponent } from './country-editor/country-editor.component';
import { ScenarioEditorComponent } from './scenario-editor.component';

@NgModule({
    declarations: [ScenarioEditorComponent, CountryEditorComponent],
    exports: [ScenarioEditorComponent],
    imports: [
        CommonModule,
        MatInputModule,
        MatExpansionModule,
        ReactiveFormsModule
    ]
})
export class ScenarioEditorModule { }
