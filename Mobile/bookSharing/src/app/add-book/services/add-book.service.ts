import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BookInformation } from '../models/book.model';

@Injectable({
  providedIn: 'root'
})
export class AddBookService {

  constructor(private http: HttpClient) { }

  getBookByIsbn(): Observable<BookInformation> {
    var url = environment.booksharingApi + '';
   
    return this.http.get<BookInformation>(url);
  }
}

