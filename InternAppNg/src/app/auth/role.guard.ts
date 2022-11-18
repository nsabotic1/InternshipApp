import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, CanActivate,} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private auth: AuthService,
    private route: ActivatedRoute, private snackBar: MatSnackBar) {

  }

  canActivate() {
    if (this.auth.hasAccess()) return true; //Checks if user is authorized
    else {
      this.snackBar.open('You are not allowed to access this page.', 'Redirecting', { duration: 3000 })
      setTimeout(() => {
        window.history.back();
      }, 3000);
      return false;
    }
  }
}
