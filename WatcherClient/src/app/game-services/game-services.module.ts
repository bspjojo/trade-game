import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GameApiService } from './game-api.service';
import { GameDataService } from './game-data.service';
import { GameHubService } from './game-hub.service';

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
