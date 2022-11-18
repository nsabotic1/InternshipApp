import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  getUsers() : Observable<any>{
    return this.http.get<any>('https://localhost:7103/api/User')
    }

  deleteUser(id: string) {
    return this.http.delete('https://localhost:7103/api/User/' + id);
  }

  addUser(user: {username: string, email: string, password: string}){
    return this.http.post('https://localhost:7103/Auth/register', user)
    .subscribe((response) => {
      console.log(response)
      this.snackBar.open('User Added Succesfully', 'Close', {
        duration: 6000
      })
    });
  }
}
