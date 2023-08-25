import { Injectable } from '@angular/core'
import { Resolve } from '@angular/router'
import { TaskService } from '../services'

@Injectable()
export class TaskListResolver implements Resolve<any> {
    constructor(private taskService:TaskService) {

    }
    resolve() {
        return this.taskService.getTasksAsync();
    }
}