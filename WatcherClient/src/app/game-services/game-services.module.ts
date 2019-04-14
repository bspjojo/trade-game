import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameHubService } from './game-hub.service';
import { GameDataService } from './game-data.service';
import { GameApiService } from './game-api.service';

@NgModule({
    declarations: [],
    imports: [
        CommonModule
    ],
    providers: [
        GameHubService,
        GameDataService,
        GameApiService
    ],
    exports: []
})
export class GameServicesModule { }
