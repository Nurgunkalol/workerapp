import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Worker, WorkerService, Group, Error } from '../../services/worker.service';

@Component({
    selector: 'worker-select',
    templateUrl: './select.component.html'
})

export class WorkerSelectComponent implements OnInit {
    constructor(private workerService: WorkerService) { }

    workers: Worker[] = [];
    newSubId: number;

    @Input() chiefId: number;

    @Output()
    selectedNewSubId = new EventEmitter<number>();

    ngOnInit(): void {
        this.workerService.getPotentialSubordinates(this.chiefId).then(workers => this.workers = workers);
    }

    newSubIdChange(args: number): void {
        this.selectedNewSubId.emit(args);
    }
}
