import { Component } from '@angular/core';
import { Worker, WorkerService, Group } from '../../services/worker.service';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Component({
    selector: 'worker-list',
    templateUrl: './list.component.html'
})
export class WorkerListComponent {
    content: Worker[] = [];
    Group: typeof Group = Group;

    constructor(private workerService: WorkerService,
        private router: Router,
        private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.getWorkers();
    }

    getWorkers(): void {
        this.workerService.getWorkers()
            .then(content => {
                this.content = content;
                let test = 0;
            })
            //error => this.error = error.json() as Error);    
    }

    edit(id: string): void {
        this.router.navigate([parseInt(id,10)], { relativeTo: this.route });
    }

    add(): void {
        this.router.navigate([0], { relativeTo: this.route });
    }

    delete(id: number): void {
        this.workerService.deleteWorker(id).then(() => this.getWorkers());
                 //   error => this.error = error.json() as Error))
    }
}