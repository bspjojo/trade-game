import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameSelectionService } from './game-selection.service';
import { AppConfigModule } from '../../app-config/app-config.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JoinGameComponent } from './join-game.component';
import { GameServicesModule } from 'src/app/game-services/game-services.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppConfigModule,
        GameServicesModule
    ],
    declarations: [JoinGameComponent],
    exports: [JoinGameComponent],
    providers: [GameSelectionService]
})
export class JoinGameModule { }
