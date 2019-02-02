import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'score-consumption-resources-display',
    templateUrl: './consumption-resources-display.component.html',
    styleUrls: ['./consumption-resources-display.component.less']
})
export class ConsumptionResourcesDisplayComponent implements OnInit {
    @Input() public heading: string;
    @Input() public consumptionObject: any;

    constructor() { }

    public ngOnInit(): void {
    }

}
