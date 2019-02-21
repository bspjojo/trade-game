import { Component, OnInit, OnDestroy, EventEmitter, Output, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs/Subject';
import { ScoringComponentService } from '../scoring-component.service';

@Component({
    selector: 'score-result-entry',
    templateUrl: './result-entry.component.html',
    styleUrls: ['./result-entry.component.less']
})
export class ResultEntryComponent implements OnInit, OnDestroy {
    public forms: { name: string, form: FormControl }[];
    public validationGroup: FormGroup;
    @Output() public resultResponse: EventEmitter<any>;
    @Input() public resetFormSubject: Subject<void>;

    private energyFormControl: FormControl;
    private chocolateFormControl: FormControl;
    private meatFormControl: FormControl;
    private grainFormControl: FormControl;
    private textilesFormControl: FormControl;

    private ngUnsubscribe: Subject<void>;

    constructor(private scoreComponentService: ScoringComponentService) {
        this.ngUnsubscribe = new Subject();
        this.resultResponse = new EventEmitter<any>();
    }

    public ngOnInit(): void {
        this.energyFormControl = new FormControl(null, [Validators.required, Validators.min(0)]);
        this.chocolateFormControl = new FormControl(null, [Validators.required, Validators.min(0)]);
        this.meatFormControl = new FormControl(null, [Validators.required, Validators.min(0)]);
        this.grainFormControl = new FormControl(null, [Validators.required, Validators.min(0)]);
        this.textilesFormControl = new FormControl(null, [Validators.required, Validators.min(0)]);

        this.forms = [
            {
                name: 'Energy',
                form: this.energyFormControl
            },
            {
                name: 'Chocolate',
                form: this.chocolateFormControl
            },
            {
                name: 'Meat',
                form: this.meatFormControl
            },
            {
                name: 'Grain',
                form: this.grainFormControl
            },
            {
                name: 'Textiles',
                form: this.textilesFormControl
            }
        ];

        this.validationGroup = new FormGroup({});

        for (let index = 0; index < this.forms.length; index++) {
            const form = this.forms[index];

            this.validationGroup.setControl(form.name, form.form);
        }

        this.resetFormSubject.takeUntil(this.ngUnsubscribe).subscribe(() => {
            this.validationGroup.reset();
        });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public async updateScores(): Promise<void> {
        this.resultResponse.emit(null);

        let requestObj = {
            Energy: this.energyFormControl.value,
            Chocolate: this.chocolateFormControl.value,
            Meat: this.meatFormControl.value,
            Grain: this.grainFormControl.value,
            Textiles: this.textilesFormControl.value
        };

        let res = await this.scoreComponentService.updateScore(requestObj);

        this.resultResponse.emit(res);
    }
}
