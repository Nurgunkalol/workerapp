import { Component, OnInit } from '@angular/core';
import { Worker, WorkerService, Group, Error } from '../../services/worker.service';
import { ActivatedRoute, Params } from '@angular/router';
 
@Component({
    selector: 'worker-edit',
    templateUrl: './edit.component.html'
})
export class WorkerEditComponent implements OnInit {
    worker: Worker;
    subordinates: Worker[] = [];
    groupWorkers = Group;
    groupSelect: string[] = [];
    newSubId: number = 0;
    salary: number = 0;
    error: Error;
    date = new Date();

    constructor(private workerService: WorkerService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.groupSelect = Object.keys(this.groupWorkers).filter(Number);
        this.route.params.forEach((params: Params) => {
            const id = parseInt(params.id, 10);
            this.getWorker(id);
            this.getSubordinates(id);
        });
    }

    getWorker(id: number): void {
        if (id === 0) {
            this.worker = new Worker();
        } else {
            this.workerService.getWorker(id).then(worker => this.worker = worker, error => this.error = error.json() as Error);
        }
    }

    getSubordinates(id: number): void {
        if (id === 0) {
            return;
        } else {
            if (this.worker.group !== 1)
                this.workerService.getSubordinates(id).then(subordinates => this.subordinates = subordinates, error => this.error = error.json() as Error)
        }
    }

    handleSubId(args: number): void {
        this.newSubId = args;
    }

    addSubordination(): void {
        if (!this.newSubId) return;
        this.workerService.createSubordinate(this.worker.id, this.newSubId).then(() => this.getSubordinates(this.worker.id), error => this.error = error.json() as Error);
    }

    cancel(): void {
        window.history.back();
    }

    onSubmit(): void {
        this.saveWorker();
    }

    deleteSub(subId: number): void {
        this.workerService.deleteSubordinate(subId).then(() => this.getSubordinates(this.worker.id), error => this.error = error.json() as Error);
    }

    saveWorker(): void {
        console.log(this.worker);
        if (this.worker.id) {
            this.workerService.updateWorker(this.worker).then(() => this.cancel(), error => this.error = error.json() as Error);
        } else {
            this.workerService.createWorker(this.worker).then(() => this.cancel(), error => this.error = error.json() as Error);
        }
    }

    getWorkerSalary(): void {
        this.workerService.getWorkerSalary(this.worker.id, this.date).then(salary => {
            this.salary = salary
        });
    }
}