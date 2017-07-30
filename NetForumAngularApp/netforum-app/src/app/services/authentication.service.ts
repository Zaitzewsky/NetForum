import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { apiUrl } from '../constants';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthenticationService {
    constructor(private http: Http) { }

    login(username: string, password: string) {
        var headers = this.createHeaders();
        var options = new RequestOptions({ headers: headers });

        var body = "grant_type=password&userName=" + username + "&password=" + password;

        return this.http.post(apiUrl + '/token', body, options)
            .map((response: Response) => {
                let authenticationInfo = response.json();
                localStorage.setItem('currentUser', authenticationInfo);
            });
    }
        
    createHeaders() {
        var headers = new Headers();
        headers.append('Accept', 'application/json');
        headers.append('Content-Type', 'application/x-www-form-urlencoded');

        return headers;
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}