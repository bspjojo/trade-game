import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ScoringModule } from './scoring/scoring.module';
import { GameSelectionModule } from './game-selection/game-selection.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        ScoringModule,
        GameSelectionModule
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
