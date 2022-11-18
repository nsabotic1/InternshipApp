import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { SelectionService } from '../selection.service';

@Component({
  selector: 'app-add-selection',
  templateUrl: './add-selection.component.html',
  styleUrls: ['./add-selection.component.css']
})
export class AddSelectionComponent implements OnInit {

  constructor(private selectionService : SelectionService, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {}


  getSelectionData(data:{name:string,StartDate:Date,EndDate:Date,description:string}){

    console.log(data);
    this.selectionService.addSelection(data);
    this.snackBar.open('New selection added successfully', 'Close');
    this.router.navigate(['/dashboard/selection']).then(() => {
      setTimeout(function() {
        window.location.reload()
      }, 2000);
    })
  }

  onCancel()
  {
    this.router.navigate(['/dashboard/selection']);
  }
}
