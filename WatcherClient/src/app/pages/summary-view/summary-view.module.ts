import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SummaryRowComponent } from './summary-row/summary-row.component';
import { SummaryViewComponent } from './summary-view.component';

@NgModule({
    declarations: [
        SummaryViewComponent,
        SummaryRowComponent
    ],
    exports: [SummaryViewComponent],
    imports: [
        CommonModule
    ]
})
export class SummaryViewModule { }
