import { AfterViewInit, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';

@Component({
    selector: 'watcher-navigation',
    templateUrl: './navigation.component.html',
    styleUrls: ['./navigation.component.less']
})
export class NavigationComponent implements OnInit, AfterViewInit {
    @ViewChild('navContainer') myDiv: ElementRef;
    @Output() width: EventEmitter<number> = new EventEmitter<number>();
    public isExpanded: boolean;

    public ngOnInit(): void {
        this.isExpanded = true;
    }

    public toggleExpanded(): void {
        this.isExpanded = !this.isExpanded;
        this.emitWidth();
    }

    public ngAfterViewInit(): void {
        this.emitWidth();
    }

    private emitWidth(): void {
        setTimeout(() => {
            let width = this.myDiv.nativeElement.offsetWidth;
            console.log('Navigation width changed to width', width);
            this.width.emit(width);
        }, 0);
    }
}
