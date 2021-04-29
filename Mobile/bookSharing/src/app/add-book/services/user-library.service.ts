import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class UserLibraryService {
    private userLibraryApiUrl: string = environment.booksharingApi + '/userlibrary/';

    constructor(private http: HttpClient) { }

    add(value: any): Observable<any> {
        return this.http.post(this.userLibraryApiUrl, value);
    }
}
