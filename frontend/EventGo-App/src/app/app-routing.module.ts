import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContatosComponent } from './components/contatos/contatos.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';

import { EventosComponent } from './components/eventos/eventos.component';
import { DetalheEventoComponent } from './components/eventos/detalhe-evento/detalhe-evento.component';
import { ListaEventoComponent } from './components/eventos/lista-evento/lista-evento.component';

import { OrganizadoresComponent } from './components/organizadores/organizadores.component';

import { UserComponent } from './components/user/user.component';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';

const routes: Routes = [
  { path: 'user/perfil', component: PerfilComponent },
  {
    path: 'user',
    component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ],
  },
  { path: 'eventos', redirectTo: 'eventos/lista' },
  {
    path: 'eventos',
    component: EventosComponent,
    children: [
      { path: 'detalhe/:id', component: DetalheEventoComponent },
      { path: 'detalhe', component: DetalheEventoComponent },
      { path: 'lista', component: ListaEventoComponent },
    ],
  },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'contatos', component: ContatosComponent },
  { path: 'organizadores', component: OrganizadoresComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
