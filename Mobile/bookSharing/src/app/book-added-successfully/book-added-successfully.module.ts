import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { BookAddedSuccessfullyPageRoutingModule } from './book-added-successfully-routing.module';

import { BookAddedSuccessfullyPage } from './book-added-successfully.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    BookAddedSuccessfullyPageRoutingModule
  ],
  declarations: [BookAddedSuccessfullyPage]
})
export class BookAddedSuccessfullyPageModule {}
