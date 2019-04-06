import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimesRepeatPipe } from './repeat/times-repeat.pipe';

@NgModule({
    declarations: [TimesRepeatPipe],
    exports: [TimesRepeatPipe],
    imports: [
        CommonModule
    ]
})
export class PipesModule { }
