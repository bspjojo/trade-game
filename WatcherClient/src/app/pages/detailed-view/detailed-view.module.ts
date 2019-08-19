import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PipesModule } from 'src/app/pipes/pipes.module';

import { DetailedViewComponent } from './detailed-view.component';

@NgModule({
    declarations: [
        DetailedViewComponent
    ],
    exports: [DetailedViewComponent],
    imports: [
        CommonModule,
        PipesModule
    ]
})
export class DetailedViewModule { }
