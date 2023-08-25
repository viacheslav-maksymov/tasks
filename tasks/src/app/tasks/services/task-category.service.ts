import { Injectable } from '@angular/core'
import { Observable, Subject } from 'rxjs';
import { ITaskCategory } from '../models/task-category.model';
import { ITaskStatus } from '../models/task-status.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable()
export class TaskCategoryService {
  constructor(private http: HttpClient) {
  }

  private apiUrl = environment.apiUrl;

  getTaskCategories():Observable<ITaskCategory[]> {
    return this.http.get<ITaskCategory[]>(`${this.apiUrl}/taskCategories`);
  }

  getTaskCategory(categoryId:number):Observable<ITaskStatus> {
    return this.http.get<ITaskStatus>(`${this.apiUrl}/taskCategories/` + categoryId);
  }
}
