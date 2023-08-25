import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router'
import { Injectable } from '@angular/core'
import { TaskService } from '../services/task.service'

@Injectable()
export class TaskRouteActivator implements CanActivate {
    constructor(private taskService:TaskService, private router: Router) {

    }

    canActivate(route:ActivatedRouteSnapshot) {
        const taskExists = !!this.taskService.getTaskAsync(+route.params['taskId'])

        if (!taskExists)
            this.router.navigate(['/404'])

        return taskExists
    } 
}