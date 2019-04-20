import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material';
import { GameServicesModule } from 'src/app/game-services/game-services.module';
import { PipesModule } from 'src/app/pipes/pipes.module';

import { AppConfigModule } from '../../app-config/app-config.module';
import { GameSelectionService } from './game-selection.service';
import { JoinGameComponent } from './join-game.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppConfigModule,
        GameServicesModule,
        MatSelectModule,
        PipesModule
    ],
    declarations: [JoinGameComponent],
    exports: [JoinGameComponent],
    providers: [GameSelectionService]
})
export class JoinGameModule { }
