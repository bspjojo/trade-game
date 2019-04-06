import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SummaryViewComponent } from './summary-view.component';
import { PipesModule } from 'src/app/pipes/pipes.module';

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
