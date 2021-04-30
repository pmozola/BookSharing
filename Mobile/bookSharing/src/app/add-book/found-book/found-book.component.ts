import { Component, Input } from '@angular/core';
import { BookInformation } from '../models/book.model';

@Component({
  selector: 'app-found-book',
  templateUrl: './found-book.component.html',
  styleUrls: ['./found-book.component.scss'],
})
export class FoundBookComponent {
  @Input() public book: BookInformation;
}
