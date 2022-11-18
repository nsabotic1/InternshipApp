import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  doLogin(credentials: Login) {
    return this.http.post<Login>('https://localhost:7103/Auth/login', credentials)
  }

  isLoggedIn() {
    return localStorage.getItem('token') != null;
  }

  getToken() {
    return localStorage.getItem('token') || '';
  }

  logOut() {
    localStorage.removeItem('token');
  }

  hasAccess() {
    // Extracting data from token
    var loginToken = localStorage.getItem('token') || '';
    var _extractedToken = loginToken.split('.')[1];
    var _atobData = atob(_extractedToken);
    var _finalData = JSON.parse(_atobData);

    const decodedRole = _finalData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (decodedRole == 'Admin') return true
    return false;
  }

  // getName() {
  //   //extracting data from token
  //   var loginToken = localStorage.getItem('token') || '';
  //   var _extractedToken = loginToken.split('.')[1];
  //   var _atobData = atob(_extractedToken);
  //   var _finalData = JSON.parse(_atobData);

  //   const decodedName = _finalData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
  //   return decodedName;
  // }
}
