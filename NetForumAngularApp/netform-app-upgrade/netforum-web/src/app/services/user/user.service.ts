import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { apiUrl } from '../../constants';
import { User } from '../../models/index';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>(apiUrl + '/api/users');
    }

    getById(id: number) {
        return this.http.get(apiUrl + '/api/users/' + id);
    }

    create(user: User) {
        return this.http.post(apiUrl + '/api/register', user);
    }

    update(user: User) {
        return this.http.put(apiUrl + '/api/users/' + user.id, user);
    }

    delete(id: number) {
        return this.http.delete(apiUrl + '/api/users/' + id);
    }
}
