import { Component, Input } from '@angular/core';

@Component({
    selector: 'model-validator',
    template: require('./model-validator.component.html')
})

export class ModelValidatorComponent {
    @Input() model: Object = new Object();
}
