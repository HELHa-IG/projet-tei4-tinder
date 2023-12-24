import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeckListComponent } from './views/deck-list/deck-list.component';
import { DeckListCardsComponent } from './views/deck-list-cards/deck-list-cards.component';
import {HistoriqueComponent} from "./views/historique/historique.component";
import {GameComponent} from "./views/game/game.component";
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ModifyProfilComponent } from './pages/modify-profil/modify-profil.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: 'deck-list', component: DeckListComponent, canActivate: [AuthGuard]},
  { path: 'deck-list-cards/:id', component: DeckListCardsComponent, canActivate: [AuthGuard]},
  { path: 'game/:id', component: GameComponent, canActivate: [AuthGuard]},
  { path: 'historique', component: HistoriqueComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'edit-profil', component: ModifyProfilComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/deck-list', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
