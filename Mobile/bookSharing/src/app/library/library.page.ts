import { Component, OnInit } from '@angular/core';
import { UserBook } from '../add-book/models/user-book.model';
import { UserLibraryService } from '../add-book/services/user-library.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.page.html',
  styleUrls: ['./library.page.scss'],
})
export class LibraryPage implements OnInit {
  userBooks: UserBook[];

  constructor(private userLibraryService: UserLibraryService) { }

  ngOnInit() {
    this.userLibraryService.get().subscribe(x => this.userBooks = x);
  }

  delete(id: number): void {
    this.userLibraryService.delete(id).subscribe(x =>
      this.userBooks = this.userBooks.filter(book => book.id != id));
  }
}
