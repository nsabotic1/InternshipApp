import { Component, Inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApplicationComment } from 'src/app/models/application-comment.model';
import { ApplicationService } from '../application.service';

@Component({
  selector: 'app-app-info',
  templateUrl: './app-info.component.html',
  styleUrls: ['./app-info.component.css']
})
export class AppInfoComponent implements OnInit, OnChanges {
  selectedValue: string;
  statuses = [
    { value: 'Applied', viewValue: 'Applied' },
    { value: 'PreSelection', viewValue: 'Pre-Selection' },
    { value: 'InSelection', viewValue: 'In Selection' },
    { value: 'Completed', viewValue: 'Completed' },
    { value: 'Rejected', viewValue: 'Rejected' }
  ]

  constructor(private appService: ApplicationService, @Inject(MAT_DIALOG_DATA) public data: any,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    console.log(this.data)
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.application) this.selectedValue = changes.application.currentValue.status; //on change refresh status
  }

  onSubmit() {
    this.data.application.status = this.selectedValue;

    this.appService.updateApplication(this.data.application)
    .subscribe(() => {
      this.snackBar.open('Successfully updated!', 'Close', {
        duration: 4000
      })
    },
     (error) => {
      this.snackBar.open(error.error.message, 'Close')
    })
  }

  onCommentSubmit(commentBody: string) {
    if (commentBody.length > 0) {
      var comment: ApplicationComment = new ApplicationComment();

      comment.body = commentBody;
      comment.applicationId = this.data.application.id;

      this.appService.addComment(comment)
      .subscribe(() => {
        this.data.onSelect()
        this.snackBar.open('Comment added successfully', 'Close');
      },
      (error) => {
        this.snackBar.open(error.error.message, 'Close')
      })
    } else {
      this.snackBar.open('Comment Cannot be empty', 'Close');
    }

  }
}

