import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScoringComponent } from './scoring.component';
import { ResultEntryComponent } from './result-entry/result-entry.component';
import { CountrySelectionComponent } from './country-selection/country-selection.component';
import { ScoringComponentService } from './scoring-component.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConsumptionResourcesDisplayComponent } from './consumption-resources-display/consumption-resources-display.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
    ],
    declarations: [
        ScoringComponent,
        ResultEntryComponent,
        CountrySelectionComponent,
        ConsumptionResourcesDisplayComponent
    ],
    exports: [
        ScoringComponent
    ],
    providers: [
        ScoringComponentService
    ]
})
export class ScoringModule { }
