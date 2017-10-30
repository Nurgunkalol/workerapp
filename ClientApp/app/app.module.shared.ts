import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { WorkerListComponent } from './components/worker/list.component';
import { WorkerEditComponent } from './components/worker/edit.component';
import { ModelValidatorComponent } from './components/worker/model-validator.component';
import { WorkerSelectComponent } from './components/worker/select.component';

import { WorkerService } from './services/worker.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        WorkerListComponent,
        WorkerEditComponent,
        ModelValidatorComponent,
        WorkerSelectComponent
    ],
    providers: [
        WorkerService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'worker', pathMatch: 'full' },
            { path: 'worker', component: WorkerListComponent },
            { path: 'worker/:id', component: WorkerEditComponent },
            { path: '**', redirectTo: 'worker' }
        ])
    ]
})
export class AppModuleShared {
}
