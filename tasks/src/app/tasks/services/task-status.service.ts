import { Injectable } from '@angular/core'
import { Observable, Subject } from 'rxjs';
import { ITaskStatus } from '../models/task-status.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable()
export class TaskStatusService {
  constructor(private http: HttpClient) {
  }

  private apiUrl = environment.apiUrl;

  getTaskStatuses():Observable<ITaskStatus[]> {
    return this.http.get<ITaskStatus[]>(`${this.apiUrl}/taskStatuses`);
  }

  getTaskStatus(statusId:number):Observable<ITaskStatus> {
    return this.http.get<ITaskStatus>(`${this.apiUrl}/taskStatuses/` + statusId);
  }
}
