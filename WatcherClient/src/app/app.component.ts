import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'watcher-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit {
    public title: string;

    public ngOnInit(): void {
        this.title = 'WatcherClient';
    }
}
