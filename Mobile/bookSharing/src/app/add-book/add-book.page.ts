import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.page.html',
  styleUrls: ['./add-book.page.scss'],
})
export class AddBookPage implements OnInit {
  recognizedBarcode: number;

  ngOnInit(): void {
  }

  onCodeBarScaned($event: number) {
    this.recognizedBarcode = $event;
    console.log($event)
  }
}
