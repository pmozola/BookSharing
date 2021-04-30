import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { WantedBooksPage } from './wanted-books.page';

const routes: Routes = [
  {
    path: '',
    component: WantedBooksPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WantedBooksPageRoutingModule {}
