import { Component, OnInit } from '@angular/core';
import { BarcodeFormat } from '@zxing/library';
import { AddBookService } from './services/add-book.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.page.html',
  styleUrls: ['./add-book.page.scss'],
})
export class AddBookPage implements OnInit {

  constructor(private addBookService: AddBookService) { }

  allowedFormats = [
    BarcodeFormat.CODABAR,
    BarcodeFormat.AZTEC,
    BarcodeFormat.CODE_39,
    BarcodeFormat.CODE_93,
    BarcodeFormat.CODE_128,
    BarcodeFormat.DATA_MATRIX,
    BarcodeFormat.EAN_8,
    BarcodeFormat.EAN_13,
    BarcodeFormat.ITF,
    BarcodeFormat.MAXICODE,
    BarcodeFormat.PDF_417,
    BarcodeFormat.UPC_A,
    BarcodeFormat.UPC_E,
    BarcodeFormat.UPC_EAN_EXTENSION
  ];

  ngOnInit() {
  }

  public scanSuccessHandler($event: any) {
    this.addBookService
      .getBookByIsbn(+$event)
      .subscribe(x => console.log(`book information from api ${x.title}`))
  }
}
