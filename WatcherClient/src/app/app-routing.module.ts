import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JoinGameComponent } from './pages/join-game/join-game.component';
import { SummaryViewComponent } from './pages/summary-view/summary-view.component';
import { DetailedViewComponent } from './pages/detailed-view/detailed-view.component';

const routes: Routes = [{
    path: '', component: JoinGameComponent
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
