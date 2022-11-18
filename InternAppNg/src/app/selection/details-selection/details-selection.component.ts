import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ApplicationService } from 'src/app/application/application.service';
import { Application } from 'src/app/models/application.model';
import { Selection } from 'src/app/models/selection.model';
import { SelectionService } from '../selection.service';

@Component({
  selector: 'app-details-selection',
  templateUrl: './details-selection.component.html',
  styleUrls: ['./details-selection.component.css']
})

export class DetailsSelectionComponent implements OnInit {

  selection:Selection;
  id: number;
  applicants: Application[] = null;
  exists:boolean = false;
  
  constructor(private selectionService : SelectionService, private applicationService : ApplicationService,private router: Router, private route: ActivatedRoute, private snackBar: MatSnackBar) {}

  ngOnInit()
  {
    this.route.params.subscribe((params: Params) =>
    {
      this.id = params['id'];
    });
    this.selectionService.getSelectionById(this.id).subscribe(response =>
    {
      this.selection = response.data;
      console.log(this.selection);
      console.log('app:',this.selection.applications);
    })
    this.applicationService.getSelectedApplicants().subscribe(response =>
    {
      this.applicants = response.data;
      console.log(this.applicants);
    }) 
  }

  OnDeleteApplicants(sId:number,aId:number,index:number)
  {
    this.selectionService.deleteSectionApplicant(sId,aId).subscribe(response =>
    {
      this.selection.applications.splice(index,1);
      this.snackBar.open('Applicant successfully deleted', 'Close');  
    })
  }

  OnAddApplicants(selectionId:number,applicantId:number,index:number)
  {
    this.selectionService.addSelectionApplicant(selectionId,applicantId).subscribe(response =>{
        for(var appl of this.selection.applications){
          if(appl.id == applicantId){
            this.snackBar.open('Applicant already exists', 'Close'); 
            setTimeout(function() {
              window.location.reload() }
              ,2000);
            this.exists = true;
            break;
          }
        }
          if(this.exists == false) {
            this.applicants.splice(index,1);
            this.selection.applications.push(response.data);
            this.snackBar.open('Applicant successfully added', 'Close');
            setTimeout(function() {
            window.location.reload() }
            ,2000);
          }
    })
  }
}
