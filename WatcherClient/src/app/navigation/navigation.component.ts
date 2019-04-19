import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'watcher-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.less']
})
export class NavigationComponent implements OnInit {
    public isExpanded: boolean;

    public ngOnInit(): void {
        this.isExpanded = true;
    }

    public toggleExpanded(): void {
        this.isExpanded = !this.isExpanded;
    }
}
