import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CurrentUser } from '../models/currentUser';
import { ApiService } from './api.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<CurrentUser>;
  public currentUser: Observable<CurrentUser>;

  constructor(private apiService: ApiService) {
    this.currentUserSubject = new BehaviorSubject<CurrentUser>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): CurrentUser {
    return this.currentUserSubject.value;
  }

  login(email: string, password: string) {
    var userLogin = {
      "Email": email,
      "Password": password
    }

    return this.apiService.post<any>('api/sessions/login', userLogin).pipe(map(token => {
      userLogin["Token"] = token;
      localStorage.setItem('currentUser', JSON.stringify(userLogin));
      var user = new CurrentUser();
      user.Email = email;
      user.Password = password;
      user.Token = token;
      this.currentUserSubject.next(user);
      return token;
    }));

  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
