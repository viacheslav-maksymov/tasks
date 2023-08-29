import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskStatusService } from '../services/task-status.service';
import { ITaskCategory } from '../models/task-category.model';
import { ITaskStatus } from '../models/task-status.model';
import { TaskService } from '../services';
import { ITask } from '../models/task.model';
import { TaskCategoryService } from '../services/task-category.service';
import { ITaskUpdate } from '../models/task-update.model';
import { ToastrService } from 'src/app/common/toastr.service';


@Component({
  selector: 'task-update-form',
  templateUrl: './task-update.component.html',
  styles: [`
      em { float:right; color:#E05C65; padding-left:10px; }
      .error input { background-color:#E3C3C5 }
  `]
})
export class UpdateTaskComponent implements OnInit {
  taskForm:FormGroup;
  categories:ITaskCategory[]; 
  statuses:ITaskStatus[];   
  task:ITask

  constructor(private fb: FormBuilder, 
    private router:Router, 
    private taskStatusService: TaskStatusService,
    private taskCategoryService: TaskCategoryService,
    private taskService: TaskService,
    private route:ActivatedRoute,
    private toastr:ToastrService) {
      this.taskForm = this.fb.group({
        title: ['', Validators.required],
        description: ['', Validators.required],
        categoryId: [''],
        statusId: [''],
    });
  }

  async ngOnInit(): Promise<any> {
    try {
      this.task = await this.taskService.getTaskAsync(+this.route.snapshot.params['taskId']);
      this.statuses = await this.taskStatusService.getTaskStatuses().toPromise();
      this.categories = await this.taskCategoryService.getTaskCategories().toPromise();

      this.taskForm.patchValue({
        title: this.task.title,
        description: this.task.description,
        categoryId: this.task.categoryId,
        statusId: this.task.statusId,
    });
  } catch (error) {
      console.error(error);
  }
  }

  update(formValues): void {
    if (this.taskForm.valid) { 
      let taskUpdate: ITaskUpdate;
  
      taskUpdate = {
        title: formValues.title,
        description: formValues.description,
        statusId: formValues.statusId,
        categoryId: formValues.categoryId
      };

      console.log(taskUpdate)
  
      this.taskService.updateTaskAsync(this.task.taskId, taskUpdate).then(
        result => {
          if (result)
          {
            console.log(this.toastr)
            this.toastr.success('Task updated successfully', this.task.title)
          }
        },
        error => {
          console.error(error)
        }
      );
    }
  }

  cancel() {
    this.router.navigate([''])
  }

  confirm() {
    return window.confirm('You have not saved this task, do you really want to cancel?')
  }d

  changes() {
    this.router.navigate(['logs/' + this.task.taskId])
  }
}
