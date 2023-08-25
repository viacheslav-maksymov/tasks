import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { TaskStatusService } from '../services/task-status.service';
import { ITaskCategory } from '../models/task-category.model';
import { ITaskStatus } from '../models/task-status.model';
import { TaskService } from '../services';
import { ITask } from '../models/task.model';
import { TaskCategoryService } from '../services/task-category.service';

@Component({
  selector: 'task-create-form',
  templateUrl: './create-task.component.html',
  styles: [`
    em { float:right; color:#E05C65; padding-left:10px; }
    .error input { background-color:#E3C3C5 }
  `]
})
export class CreateTaskComponent implements OnInit {
  taskForm: FormGroup;
  categories:ITaskCategory[]; 
  statuses:ITaskStatus[];   

  constructor(private fb: FormBuilder, 
    private http: HttpClient, 
    private router:Router, 
    private taskStatusService: TaskStatusService,
    private taskCategoryService: TaskCategoryService,
    private taskService: TaskService) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      categoryId: [],
      statusId: [0],
    });
  }

  ngOnInit(): void {
    this.taskStatusService.getTaskStatuses().toPromise().then(
      data => {
        this.statuses = data
      }
    ).catch(error => {
      console.error(error)
    });

    this.taskCategoryService.getTaskCategories().toPromise().then(
      data => {
        this.categories = data
      }
    ).catch(error => {
      console.error(error)
    });
  }

  create(formValues): void {
    if (this.taskForm.valid) { 
      let task: ITask;
  
      task = {
        title: formValues.title,
        description: formValues.description,
        statusId: formValues.statusId,
        categoryId: formValues.categoryId
      };

      console.log(task)
  
      this.taskService.createTaskAsync(task).then(
        result => {
          this.taskForm.reset()
          this.router.navigate(['/tasks']);
        },
        error => {

        }
      );
    }
  }

  cancel() {
    this.router.navigate([''])
  }

  confirm() {
    return window.confirm('You have not saved this task, do you really want to cancel?')
}
}
