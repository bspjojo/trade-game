import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'score-consumption-resources-display',
    templateUrl: './consumption-resources-display.component.html',
    styleUrls: ['./consumption-resources-display.component.less']
})
export class ConsumptionResourcesDisplayComponent implements OnInit {
    @Input() public heading: string;
    @Input() public consumptionObject: any;

    @Input() public initialShow: boolean;
    public show: boolean;

    public ngOnInit(): void {
        this.show = this.initialShow;
    }
}
