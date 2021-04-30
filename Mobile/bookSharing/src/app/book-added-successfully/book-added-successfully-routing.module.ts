import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BookAddedSuccessfullyPage } from './book-added-successfully.page';

const routes: Routes = [
  {
    path: '',
    component: BookAddedSuccessfullyPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BookAddedSuccessfullyPageRoutingModule {}
