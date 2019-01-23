import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ScoringModule } from './scoring/scoring.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        ScoringModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
