import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { Login } from '../models/login.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup
  responseData: any
  wrongCred: boolean = false;

  constructor(private auth: AuthService,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    if (this.auth.isLoggedIn) {
      this.router.navigate(['/dashboard'])
    }
  }

  onSubmit(loginForm: Login) {
    this.auth.doLogin(loginForm)
    .subscribe(
      (result) => {
        if (result != null) {
          this.responseData = result;
          console.log(this.responseData)
          localStorage.setItem('token', this.responseData.token); //sets token in local storage
          this.router.navigate(['/dashboard/application'])
        }
      },
       (error) => {
        if (error.status == 401) {
          this.wrongCred = true;
          this.snackBar.open('Wrong credentials!', 'Close')
        }
      }
    )
  }
}


