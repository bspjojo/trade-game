import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { GameDataService } from 'src/app/game-services/game-data.service';
import { Game } from 'src/app/game-services/game.model';

@Component({
    selector: 'watcher-detailed-view',
    templateUrl: './detailed-view.component.html',
    styleUrls: ['./detailed-view.component.less', './../shared/scores-table-less/scores-table.less']
})
export class DetailedViewComponent implements OnInit, OnDestroy {
    private ngUnsubscribe: Subject<void>;
    public game: Game;

    constructor(private gameDataService: GameDataService) {
        this.ngUnsubscribe = new Subject<void>();
    }

    public ngOnInit(): void {
        this.gameDataService.gameSubject
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((gameData: Game) => this.game = gameData);
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
