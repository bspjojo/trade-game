import { Component, OnInit, OnDestroy } from '@angular/core';
import { GameDataService } from 'src/app/game-services/game-data.service';
import { Subject } from 'rxjs';
import { Game } from 'src/app/game-services/game.model';
import { takeUntil } from 'rxjs/operators';

@Component({
    selector: 'watcher-detailed-view',
    templateUrl: './detailed-view.component.html',
    styleUrls: ['./detailed-view.component.less']
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