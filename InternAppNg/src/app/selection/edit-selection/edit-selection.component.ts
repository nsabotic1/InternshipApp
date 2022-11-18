import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Selection } from 'src/app/models/selection.model';
import { SelectionService } from '../selection.service';

@Component({
  selector: 'app-edit-selection',
  templateUrl: './edit-selection.component.html',
  styleUrls: ['./edit-selection.component.css']
})

export class EditSelectionComponent implements OnInit {

  selection:Selection;
  id: number;
  name?:string;
  start?:Date;
  end?:Date;
  description?:string;

  constructor(private selectionService : SelectionService, private router: Router, private route: ActivatedRoute, private snackBar: MatSnackBar) { }

  ngOnInit(): void
  {
    this.route.params.subscribe((params: Params) => {
    this.id = +params['id'];
    });
    this.selectionService.getSelectionById(this.id).subscribe(response =>
      {
       this.selection = response.data;
       this.name=this.selection.name;
       this.start=this.selection.startDate;
       this.end=this.selection.endDate;
       this.description=this.selection.description;
       console.log(this.selection);
       console.log(this.name);
       
      })
  }
  
  onUpdateSelection(editSelectionForm: NgForm)
  {
    const id = this.id;
    const name = editSelectionForm.value.name;
    const startDate = editSelectionForm.value.StartDate;
    const endDate = editSelectionForm.value.EndDate;
    const description = editSelectionForm.value.description;

    this.selectionService.updateSelection(id,name,startDate,endDate,description).subscribe(response =>
    {
      this.snackBar.open('Selection successfully updated', 'Close');
      this.router.navigate(['/dashboard/selection']);
    })
  }

  onCancel()
  {
    this.router.navigate(['/dashboard/selection']);
  }
}