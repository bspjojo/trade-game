import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailedViewComponent } from './detailed-view.component';
import { PipesModule } from 'src/app/pipes/pipes.module';

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
