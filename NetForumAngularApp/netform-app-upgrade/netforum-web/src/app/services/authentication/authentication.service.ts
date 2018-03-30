import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { apiUrl } from '../../constants';
import { User } from '../../models';

@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(username: string, password: string) {
        const headers = this.createHeaders();

        const body = 'grant_type=password&userName=' + username + '&password=' + password;

        return this.http.post<any>(apiUrl + '/token', body, { headers: headers } )
            .map(user => {
                // login successful if there's a jwt token in the response
                if (user && user.token) {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            });
    }

    createHeaders() {
        const headers = new HttpHeaders();
        headers.append('Accept', 'application/json');
        headers.append('Content-Type', 'application/x-www-form-urlencoded');

        return headers;
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}
