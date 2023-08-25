import { Component, Input, Output, EventEmitter } from '@angular/core'
import { ITask } from 'src/app/tasks/models/task.model'


@Component({
    selector: 'task-thumbnail',
    templateUrl: 'task-thumbnail.component.html' 
})

export class TaskThumbnailComponent{
    @Input() task:ITask

}