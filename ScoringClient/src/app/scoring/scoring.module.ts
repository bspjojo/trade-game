import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScoringComponent } from './scoring.component';
import { ResultEntryComponent } from './result-entry/result-entry.component';
import { ExcessDisplayComponent } from './excess-display/excess-display.component';
import { CountrySelectionComponent } from './country-selection/country-selection.component';
import { NextYearTargetDisplayComponent } from './next-year-target-display/next-year-target-display.component';
import { ScoringComponentService } from './scoring-component.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
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
    ],
    providers: [
        ScoringComponentService
    ]
})
export class ScoringModule { }
