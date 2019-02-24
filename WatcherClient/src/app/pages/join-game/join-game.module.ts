import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameSelectionService } from './game-selection.service';
import { AppConfigModule } from '../../app-config/app-config.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JoinGameComponent } from './join-game.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppConfigModule
    ],
    declarations: [JoinGameComponent],
    exports: [JoinGameComponent],
    providers: [GameSelectionService]
})
export class JoinGameModule { }
