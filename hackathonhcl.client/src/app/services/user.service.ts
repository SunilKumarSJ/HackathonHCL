import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { auth } from '../models/user';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  myAppUrl: string = "https://localhost:7038/api/User/";
  tokenKey = 'token';
  constructor(private _http: HttpClient) {

  }

  getAll(): Observable<any> {
    return this._http.get<any>(this.myAppUrl + 'GetAll', { withCredentials: true }).pipe(map((data) => {
      return data;
    }))
  }

  login(auth: auth) {
    return this._http.post<any>(this.myAppUrl + 'login', auth).pipe(map((data) => {
      return data;
    }))
  }

  saveToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  //
  getjwtDecode() {
    const token = this.getToken();
    if (!token) return '';

    const decoded: any = jwtDecode(token);
    return decoded;
  }

  getRoles(): string {
    const token = this.getToken();
    if (!token) return '';

    const decoded: any = this.getjwtDecode();

    //if (Array.isArray(decoded.role)) {
    //  console.log(decoded.role)
    //  return decoded.role;
    //} else if (typeof decoded.role === 'string') {
    //  console.log([decoded.role])
    //  return [decoded.role];
    return decoded.role;
  }

  getLoggedInUserId() {
    const decoded: any = this.getjwtDecode();//
    return decoded.given_name;
  }

  isInRole(roles: string[]): boolean {
    if (this.getRoles()?.length) {
      return roles.includes(this.getRoles());
    }
    else {
      return false;
    }
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (token == 'null' || !token) { return false }

    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return Date.now() < expiry * 1000;
  }

  logout() {
    localStorage.removeItem('token');
  }

}

