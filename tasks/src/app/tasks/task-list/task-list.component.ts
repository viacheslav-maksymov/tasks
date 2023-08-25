import { Component, OnInit } from '@angular/core'
import { TaskService } from '../services/task.service'
import { ToastrService } from '../../common/toastr.service'
import { ActivatedRoute } from '@angular/router'
import { ITask } from 'src/app/tasks/models/task.model'

@Component({
    selector: 'task-list',
    templateUrl: 'task-list.component.html'
})

export class TaskListComponent implements OnInit {
    tasks:ITask[]

    constructor(private taskService: TaskService, private toastrService: ToastrService,
        private route: ActivatedRoute) {
        
      }

    ngOnInit() {
        this.tasks = this.route.snapshot.data['tasks']
      }
}