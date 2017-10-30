import { Injectable } from '@angular/core';
import { Headers, Http, URLSearchParams } from '@angular/http';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class WorkerService {
    private workerUrl = '/api/worker';  // URL to web api
    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    getWorkers(): Promise<Worker[]> {
        const url = this.workerUrl;

        const params = new URLSearchParams();

        return this.http.get(url, {
            search: params
        }).toPromise()
            .then(response => response.json() as Worker[])
            .catch(this.handleError);
    }

    getWorker(id: number): Promise<Worker> {
        const url = `${this.workerUrl}/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as Worker)
            .catch(this.handleError);
    }

    getSubordinates(id: number): Promise<Worker[]> {
        const url = `${this.workerUrl}/subordinates/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as Worker[])
            .catch(this.handleError);
    }

    getPotentialSubordinates(id: number): Promise<Worker[]> {
        const url = `${this.workerUrl}/potentialsubs/${id}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as Worker[])
            .catch(this.handleError);
    }

    createSubordinate(workerId: number, newSubId: number): Promise<void> {
        const url = `${this.workerUrl}/createsub/${workerId}/${newSubId}`;
        return this.http.get(url)
            .toPromise()
            .then(response => response.json() as Worker[])
            .catch(this.handleError);
    }

    updateWorker(worker: Worker): Promise<Worker> {
        const url = `${this.workerUrl}/${worker.id}`;
        return this.http.put(url, JSON.stringify(worker), { headers: this.headers })
            .toPromise()
            .then(() => worker)
            .catch(this.handleError);
    }

    createWorker(worker: Worker): Promise<Worker> {
        return this.http.post(this.workerUrl, JSON.stringify(worker), { headers: this.headers })
            .toPromise()
            .then(() => Worker)
            .catch(this.handleError);
    }

    deleteWorker(id: number): Promise<void> {
        const url = `${this.workerUrl}/${id}`;
        return this.http.delete(url)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
}

export class Worker {
    id: number;
    name: string;
    group: Group;
    entryWorkDate: Date = new Date();
    baseRate: number;
    chiefId: number;
    chief: Worker;
}

export enum Group {
    Employee = 1,
    Manager = 2,
    Salesman = 3,
}

export class Error {
    message: string;
    modelState: Object;
    exceptionMessage: string;
    exceptionType: string;
    errors: Object[];
}

