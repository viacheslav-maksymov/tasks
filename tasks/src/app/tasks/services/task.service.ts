import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Observable, Subject } from 'rxjs'
import { ITask } from 'src/app/tasks/models/task.model';
import { UserService } from 'src/app/user/services/user.service';
import { environment } from 'src/environments/environment';
import { ITaskUpdate } from '../models/task-update.model';

@Injectable()
export class TaskService {
  constructor(private http: HttpClient,
    private userService: UserService) {
  }

  private apiUrl = environment.apiUrl;

  async getTasksAsync(): Promise<ITask[]> {
    console.log('getTasksAsync')
    const userId = await this.userService.getUser().userId;
    console.log(userId)
    const response = await this.http.get<ITask[]>(`${this.apiUrl}/tasks`).toPromise();
    console.log(response)
    return response;
  }

  async getTaskAsync(taskId:number): Promise<ITask> {
    const response = await this.http.get<ITask>(`${this.apiUrl}/tasks/${taskId}`).toPromise();
    return response;
  }

  async updateTaskAsync(id:number, task:ITaskUpdate): Promise<boolean> {
    const response = await this.http.put<any>(`${this.apiUrl}/tasks/` + id, task, { observe: 'response' }).toPromise();
    return response.status === 204;
  }
    
    async createTaskAsync(task: ITask):Promise<boolean> {
      try {
        
        let user = await this.userService.getUser();
        task.userId = user.userId;

        const response = await this.http.post<any>(`${this.apiUrl}/tasks`, task, { observe: 'response' }).toPromise();
        return response.status === 201;
      } catch (error) {
        console.error('An error occurred:', error);
        return false;
      }
    }
}