import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminComponent } from './pages/admin/admin.component';
import { DetailedViewComponent } from './pages/detailed-view/detailed-view.component';
import { JoinGameComponent } from './pages/join-game/join-game.component';
import { SummaryViewComponent } from './pages/summary-view/summary-view.component';

const routes: Routes = [{
    path: '', component: JoinGameComponent
}, {
    path: 'admin', component: AdminComponent
}, {
    path: 'scores', children: [{
        path: 'summary', component: SummaryViewComponent
    }, {
        path: 'detailed', component: DetailedViewComponent
    }]
}];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
