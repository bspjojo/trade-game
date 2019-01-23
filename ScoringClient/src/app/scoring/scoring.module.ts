import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScoringComponent } from './scoring.component';
import { ResultEntryComponent } from './result-entry/result-entry.component';
import { ExcessDisplayComponent } from './excess-display/excess-display.component';
import { CountrySelectionComponent } from './country-selection/country-selection.component';
import { NextYearTargetDisplayComponent } from './next-year-target-display/next-year-target-display.component';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
        ScoringComponent,
        ResultEntryComponent,
        ExcessDisplayComponent,
        CountrySelectionComponent,
        NextYearTargetDisplayComponent
    ],
    exports: [
        ScoringComponent
    ]
})
export class ScoringModule { }
