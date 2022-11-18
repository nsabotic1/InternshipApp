import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar'; import { Sort } from '@angular/material/sort';
import { ApplicationComment } from '../models/application-comment.model';
import { Application } from '../models/application.model';
import { AppInfoComponent } from './app-info/app-info.component';
import { ApplicationService } from './application.service';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css']
})
export class ApplicationComponent implements OnInit {
  application: Application;
  comments!: Array<ApplicationComment>;
  applications: Application[] = [];
  selections: Selection[] = [];
  search: string;
  page: number = 1;
  total: number = 0;
  selectedChip: number = 0;
  statusChipId: number = 0;
  sortType: string;
  showSpinner: boolean = true;
  rowSelected: Boolean = false;

  constructor(private appService: ApplicationService, private snackBar: MatSnackBar, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getApplications();
  }

  getApplications() {
    this.appService.getApplicationsPaged(this.page, this.search, this.selectedChip, this.statusChipId, this.sortType)
    .subscribe(data => {
      this.applications = data.data.data;
      this.total = data.data.totalPages;
      this.showSpinner = false;
    })
  }

  pageChangeEvent(event: number) {
    this.page = event;
    this.getApplications();
  }

  onSelect(application: Application) {
    this.application = application;

    this.appService.getByApplicationId(application.id)
      .subscribe((response) => {
        this.comments = response.data.comments;
        this.rowSelected = true;
        this.selections = response.data.selections;
        this.dialog.closeAll()
        this.openDialog()
      }, () => {
        this.snackBar.open('User Not Found', 'Close')
      })
  }

  applyFilter(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    this.search = inputValue;
    this.getApplications();
  }

  changeSelectedChip(id: number) {
    this.selectedChip = id;
    this.getApplications();
  }

  statusChip(id: number) {
    this.statusChipId = id;
    this.getApplications();
  }

  resetChips() {
    this.statusChipId = 0;
    this.selectedChip = 0;
    this.search = null;
    this.getApplications();
  }

  sortData(event: Sort) {
    this.sortType = event.active + event.direction;
    console.log(this.sortType)
    this.getApplications();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AppInfoComponent, {
      height: '80%',
      width: '80%',
      data: {
        application: this.application, //passed from parent component to modal
        comments: this.comments,
        selections: this.selections,
        onSelect: () => this.onSelect(this.application)
      }
    });
  }


}
