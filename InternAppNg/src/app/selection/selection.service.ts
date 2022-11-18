import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SelectionComment } from '../models/selection-comment.model';
import { Selection } from '../models/selection.model';

@Injectable({
  providedIn: 'root'
})

export class SelectionService {

  constructor(private http: HttpClient) {}

  getSelections(page:number,name?:string, description?:string)
  {

    if(name!=null && description==null) return this.http.get<any>("https://localhost:7103/api/Selection/GetPage" + '?pageIndex='
    + page + '&pageSize='+5
    +"&name="+name);

    else if(description!=null && name==null) return this.http.get<any>("https://localhost:7103/api/Selection/GetPage" + '?pageIndex='
    + page + '&pageSize='+5+"&description="+description);

    else if(description!=null && name!=null) return this.http.get<any>("https://localhost:7103/api/Selection/GetPage" + '?pageIndex='
    + page + '&pageSize='+5+"&name="+name+"&description="+description);

    else return this.http.get<any>("https://localhost:7103/api/Selection/GetPage" + '?pageIndex='
    + page + '&pageSize='+5);
  }

  addSelection(selection:{name:string,StartDate:Date,EndDate:Date,description:string})
  {
    return this.http.post<Selection>("https://localhost:7103/api/Selection",selection).subscribe((p)=>
    console.log(p));
  }

  getSelectionById(id:number)
  {
    return this.http.get<any>("https://localhost:7103/api/Selection/" + id);
  }

  updateSelection(id:number,name:string,StartDate:Date,EndDate:Date,description:string)
  {
    return this.http.put<any>( 'https://localhost:7103/api/Selection/update',{
      id:id,
      name: name,
      startDate: StartDate,
      endDate: EndDate,
      description:description
    })}

  getCommentsByUserId(id: number) : Observable<any>
  {
    return this.http.get<any>('https://localhost:7103/api/Selection/comments/' + id);
  }

  addComment(comment: SelectionComment)
  {
    return this.http.post<SelectionComment>('https://localhost:7103/api/Selection/comments', comment);
  }

  deleteSectionApplicant(sId:number,aId:number){
    return this.http.delete<any>("https://localhost:7103/api/Selection/applicants?selectionId=" + sId +"&applicantId=" + aId);
   }

  addSelectionApplicant(sId:number,aId:number)
  {
   return this.http.post<any>("https://localhost:7103/api/Selection/Applicant",
   {
      selectionId: sId,
      applicationId: aId
   })
  }

}
