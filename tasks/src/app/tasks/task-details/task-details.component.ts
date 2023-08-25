import { Component } from "@angular/core"
import { TaskService } from "../services/task.service"
import { ActivatedRoute } from '@angular/router'
import { ITask } from "src/app/tasks/models/task.model"

@Component({
    selector: 'task-details',
    templateUrl:'./task-details.component.html',
    styles: [`
        .container { padding-left:20px; padding-right:20px; }
        .task-image { height: 100px; }
    `]
})

export class TaskDetailsComponent {
    task:ITask
    constructor(private taskService:TaskService, 
        private route:ActivatedRoute) { 

    }
    
    ngOnInit() {
        this.taskService.getTaskAsync(+this.route.snapshot.params['taskId']).then(
            result => {
                this.task = result
            }
        )
    }

}