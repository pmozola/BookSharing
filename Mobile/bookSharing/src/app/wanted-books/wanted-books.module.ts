import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { WantedBooksPageRoutingModule } from './wanted-books-routing.module';

import { WantedBooksPage } from './wanted-books.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    WantedBooksPageRoutingModule
  ],
  declarations: [WantedBooksPage]
})
export class WantedBooksPageModule {}
