import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class UserService {
  users: User[];

  constructor(private apiService: ApiService) { }

  getUsers() {
    return this.apiService.get<User[]>('api/users')
      .pipe(map(data => {
        this.users = data;
      }));
  }

  updateUser(user: User) {
    return this.apiService.put('api/users', {"Email": user.email, "FullName": user.fullName});
  }
}
