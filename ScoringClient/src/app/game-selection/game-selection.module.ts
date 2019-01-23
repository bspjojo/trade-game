import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameSelectionComponent } from './game-selection.component';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [GameSelectionComponent],
    exports: [GameSelectionComponent]
})
export class GameSelectionModule { }
