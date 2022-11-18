import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApplicationComment } from '../models/application-comment.model';
import { Application } from '../models/application.model';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private http: HttpClient) {
  }

  getApplicationsPaged(page: number, filter?: string, eduLevel?: number, status?: number, sort?: string) {
    let queryParams = new HttpParams();

    queryParams = queryParams.append('pageIndex', page);
    queryParams = queryParams.append('pageSize', 5);
    if (filter) queryParams = queryParams.append('searchName', filter);
    if (eduLevel) queryParams = queryParams.append('eduLevel', eduLevel);
    if (eduLevel === 0) queryParams = queryParams.delete('eduLevel');
    if (status) queryParams = queryParams.append('status', status);
    if (status === 0) queryParams = queryParams.delete('status');
    if (sort) queryParams = queryParams.append('sort', sort)

    return this.http.get<any>("https://localhost:7103/api/Application/GetPage", { params: queryParams });
  }

  getApplications(): Observable<Application> {
    return this.http.get<Application>('https://localhost:7103/api/Application')
  }

  getSelectedApplicants() {
    return this.http.get<any>('https://localhost:7103/api/Application/GetSelected')
  }

  storeApplication(postData: { firstName: string, lastName: string, email: string, educationLevel: number, coverLetter: string, cv: string }) {
    this.http.post<Application>('https://localhost:7103/api/Application', postData)
    .subscribe();
  }

  getByApplicationId(id: number): Observable<any> {
    return this.http.get<any>('https://localhost:7103/api/Application/' + id);
  }

  updateApplication(application: Application) {
    return this.http.put<Application>('https://localhost:7103/api/Application', application);
  }

  addComment(comment: ApplicationComment) {
    return this.http.post<ApplicationComment>('https://localhost:7103/api/Application/Comment', comment);
  }
}
