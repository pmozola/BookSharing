import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { concatMap, tap } from 'rxjs/operators';

import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private http: HttpClient) {
    }

    private bookApiUrl: string = environment.booksharingApi + '/Auth/';

    login(email: string, password: string) {
        return this.http.post<User>(this.bookApiUrl + 'login', { email, password })
            .pipe(tap(val => this.setSession(val)))

    }

    registerAndLogin(email: string, userName: string, password: string) {
        return this.http.post<any>(this.bookApiUrl + 'register', { userName, email, password })
            .pipe(
                concatMap(_ => this.login(email, password))
            )
    }

    private setSession(authResult) {
        localStorage.setItem('id_token', authResult.token);
        localStorage.setItem("expires_at", authResult.expiration);
    }

    logout() {
        localStorage.removeItem("id_token");
        localStorage.removeItem("expires_at");
    }

    public isLoggedIn() {
        var isDateValid = new Date() < this.getExpiration()
        
        return isDateValid
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
        const expiration = localStorage.getItem("expires_at");

        return  new Date(expiration);
    }
}
