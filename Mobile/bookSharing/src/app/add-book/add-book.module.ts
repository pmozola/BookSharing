import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { AddBookPageRoutingModule } from './add-book-routing.module';

import { AddBookPage } from './add-book.page';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { BarCodeReaderComponent } from './bar-code-reader/bar-code-reader.component';
import { FoundBookComponent } from './found-book/found-book.component';
import { SaveBookComponent } from './save-book/save-book.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AddBookPageRoutingModule,
    ZXingScannerModule,
    ReactiveFormsModule
  ],
  declarations: [
    AddBookPage,
    BarCodeReaderComponent,
    FoundBookComponent,
    SaveBookComponent,
  ]
})
export class AddBookPageModule { }
