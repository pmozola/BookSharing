import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { tap } from 'rxjs/operators';

import * as moment from "moment";
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient) {
    }
    private bookApiUrl: string = environment.booksharingApi + '/Auth/';
    login(userName: string, password: string) {
        return this.http.post<User>(this.bookApiUrl +'login', { userName, password })
            .pipe(tap(val => this.setSession(val)))
        // this is just the HTTP call, 
        // we still need to handle the reception of the token

    }

    private setSession(authResult) {
        console.log(authResult);
        const expiresAt = moment().add(authResult.expiration, 'second');

        localStorage.setItem('id_token', authResult.token);
        localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
    }

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");
    }

    public isLoggedIn() {
        return moment().isBefore(this.getExpiration());
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_at");
        const expiresAt = JSON.parse(expiration);
        return moment(expiresAt);
    }
}
