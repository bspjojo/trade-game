import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PipesModule } from 'src/app/pipes/pipes.module';

import { SummaryViewComponent } from './summary-view.component';

@NgModule({
    declarations: [
        SummaryViewComponent
    ],
    exports: [SummaryViewComponent],
    imports: [
        CommonModule,
        PipesModule
    ]
})
export class SummaryViewModule { }
