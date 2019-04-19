import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { NavigationComponent } from './navigation.component';

@NgModule({
    declarations: [
        NavigationComponent
    ],
    exports: [
        NavigationComponent
    ],
    imports: [
        BrowserModule,
        RouterModule
    ]
})
export class NavigationModule { }
