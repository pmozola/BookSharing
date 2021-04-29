import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserBook } from '../models/user-book.model';

@Injectable({
    providedIn: 'root'
})
export class UserLibraryService {
    private userLibraryApiUrl: string = environment.booksharingApi + '/userlibrary/';

    constructor(private http: HttpClient) { }

    add(value: any): Observable<any> {
        return this.http.post(this.userLibraryApiUrl, value);
    }

    get(): Observable<UserBook[]> {
        return this.http.get<UserBook[]>(this.userLibraryApiUrl);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(this.userLibraryApiUrl + id)
    }
}
