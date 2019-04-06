import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'timesRepeat'
})
export class TimesRepeatPipe implements PipeTransform {
    transform(value: number, startIndex: number = 0): any {
        const iterable = {};
        // tslint:disable-next-line:space-before-function-paren
        iterable[Symbol.iterator] = function* (): IterableIterator<any> {
            for (let index = 0; index < value; index++) {
                yield (startIndex + index);
            }
        };
        return iterable;
    }

}
