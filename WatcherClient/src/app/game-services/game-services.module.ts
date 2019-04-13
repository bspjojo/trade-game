import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameHubService } from './game-hub.service';
import { GameDataService } from './game-data.service';

@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ],
    providers: [
        GameHubService,
        GameDataService
    ],
    exports: []
})
export class GameServicesModule { }
