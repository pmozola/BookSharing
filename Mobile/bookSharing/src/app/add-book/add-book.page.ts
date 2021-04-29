import { Component, OnInit } from '@angular/core';
import { BookInformation } from './models/book.model';
import { AddBookService } from './services/add-book.service';


@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.page.html',
  styleUrls: ['./add-book.page.scss'],
})
export class AddBookPage implements OnInit {
  recognizedBarcode: number;
  book: BookInformation;

  constructor(private addbookService: AddBookService) { }
  ngOnInit(): void { }

  onCodeBarScaned($event: number) {
    this.recognizedBarcode = $event;
    if ($event) {
      this.addbookService.getBookByIsbn($event).subscribe(x => this.book = x)
    }
  }
}
