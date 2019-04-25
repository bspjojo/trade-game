import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { AdminComponent } from './admin.component';

@NgModule({
    declarations: [AdminComponent],
    exports: [AdminComponent],
    imports: [
        CommonModule
    ]
})
export class AdminModule { }
