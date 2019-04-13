import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JoinGameModule } from './pages/join-game/join-game.module';
import { SummaryViewModule } from './pages/summary-view/summary-view.module';
import { NavigationModule } from './navigation/navigation.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        JoinGameModule,
        SummaryViewModule,
        BrowserModule,
        AppRoutingModule,
        NavigationModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
