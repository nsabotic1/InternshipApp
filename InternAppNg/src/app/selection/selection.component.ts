import { Component, EventEmitter, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionComment } from '../models/selection-comment.model';
import { Selection } from '../models/selection.model';
import { SelectionCommentInfoComponent } from './selection-comment-info/selection-comment-info.component';
import { SelectionService } from './selection.service';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.css']
})

export class SelectionComponent implements OnInit {

  selections:Selection[] = [];
  page: number = 1;
  total: number = 0;
  search?: string;
  description?:string;
  rowSelected : boolean = false;
  comments!: Array<SelectionComment>;
  selection: Selection;
  isSelected = false;

  constructor(private selectionService : SelectionService, private snackBar: MatSnackBar, private dialog: MatDialog) {}

  ngOnInit(): void
  {
    this.getSelections();
  }

  getSelections()
  {
    this.selectionService.getSelections(this.page, this.search,this.description).subscribe(data =>
    {
    this.selections =data.data.data;
    this.total=data.data.totalPages;
    console.log(this.selections);
    })
  }

  onSelect(selection: Selection)
  {
    this.selection = selection;
    this.selectionService.getCommentsByUserId(selection.id)
    .subscribe((response) => {
      this.comments = response.data;
      this.rowSelected=true;
    }, () =>
    {
      this.snackBar.open('User Not Found', 'Close')
    })
  }
  
  nameFilter(event:Event)
  {
      const inputValue = (event.target as HTMLInputElement).value;
      this.search=inputValue;
      console.log(this.search);
      this.getSelections();
  }

  descriptionFilter(event:Event)
  {
      const inputValue = (event.target as HTMLInputElement).value;
      this.description=inputValue;
      console.log(this.description);
      this.getSelections();
  }

  pageChangeEvent(event: number)
  {
      this.page = event;
      this.getSelections();
  }

  resetFilters()
  {
    this.description=null;
    this.search = null;
    this.getSelections();
  }

  openDialog(): void
  {
      const dialogRef = this.dialog.open(SelectionCommentInfoComponent, {
        height: '80%',
        width: '80%',
        data:{
            selection: this.selection,
            comments: this.comments,
            onSelect: () => this.onSelect(this.selection)
        }
      });
  }
}
