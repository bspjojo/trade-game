import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationModule } from './navigation/navigation.module';
import { DetailedViewModule } from './pages/detailed-view/detailed-view.module';
import { JoinGameModule } from './pages/join-game/join-game.module';
import { SummaryViewModule } from './pages/summary-view/summary-view.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        JoinGameModule,
        SummaryViewModule,
        DetailedViewModule,
        BrowserModule,
        AppRoutingModule,
        NavigationModule,
        NoopAnimationsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
