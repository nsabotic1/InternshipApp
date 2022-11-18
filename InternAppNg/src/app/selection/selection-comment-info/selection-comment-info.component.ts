import { Component, EventEmitter, Inject, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SelectionComment } from 'src/app/models/selection-comment.model';
import { Selection } from 'src/app/models/selection.model';
import { SelectionService } from '../selection.service';

@Component({
  selector: 'app-selection-comment-info',
  templateUrl: './selection-comment-info.component.html',
  styleUrls: ['./selection-comment-info.component.css']
})

export class SelectionCommentInfoComponent implements OnInit {

  @Input() comments: SelectionComment[];
  @Input() selection: Selection;
  @Output("onSelect") onSelect: EventEmitter<any> = new EventEmitter();

  constructor(private selectionService: SelectionService, private snackBar: MatSnackBar) { }

  ngOnInit(): void
  {
    console.log(this.comments);
  }

  onCommentSubmit(commentBody: string)
  {
    if (commentBody.length > 0)
    {
      var comment: SelectionComment = new SelectionComment();
      comment.body = commentBody;
      comment.selectionId = this.selection.id;
      console.log(comment);
      this.selectionService.addComment(comment).subscribe((response) => {
      this.snackBar.open('Comment added successfully', 'Close');
      setTimeout(function() {
        window.location.reload() }
        ,2000);
      
      this.onSelect.emit();
      }, (error) =>
      {
        this.snackBar.open(error.error.message, 'Close')
      })
    } else
    {
      this.snackBar.open('Comment Cannot be empty', 'Close');
    }
  }
}
