import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserProfilPage } from './user-profil.page';

const routes: Routes = [
  {
    path: '',
    component: UserProfilPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserProfilPageRoutingModule {}
