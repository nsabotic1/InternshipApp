import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ApplicationService } from '../application/application.service';

@Component({
  selector: 'app-application-form',
  templateUrl: './application-form.component.html',
  styleUrls: ['./application-form.component.css']
})
export class ApplicationFormComponent{

  constructor(private appService: ApplicationService, private router: Router, private snackBar : MatSnackBar) { }

  onSubmit(applicationData: {firstName: string, lastName: string, email: string, educationLevel: number, coverLetter: string, cv: string}){
    this.appService.storeApplication(applicationData);
    this.router.navigate(['home']);
    this.snackBar.open('Thank you for applying!', 'Close', {
      duration: 6000
    })
  }
}
