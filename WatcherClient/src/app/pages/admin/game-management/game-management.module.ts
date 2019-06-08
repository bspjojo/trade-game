import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GameManagementComponent } from './game-management.component';

@NgModule({
    declarations: [GameManagementComponent],
    exports: [GameManagementComponent],
    imports: [
        CommonModule
    ]
})
export class GameManagementModule { }
