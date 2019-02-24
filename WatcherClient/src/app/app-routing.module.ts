import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JoinGameComponent } from './pages/join-game/join-game.component';

const routes: Routes = [{
    path: '', component: JoinGameComponent
}];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
