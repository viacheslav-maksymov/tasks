import { NgModule } from "@angular/core";
import { RouterModule } from '@angular/router';
import { TaskListComponent } from "./task-list/task-list.component";
import { TaskThumbnailComponent } from "./task-thumbnail/task-thumbnail.component";
import { TaskDetailsComponent, TaskRouteActivator } from "./task-details";
import { CreateTaskComponent } from "./task-create/create-task.component";
import { TaskService } from "./services";
import { TaskListResolver } from "./task-list/task-list-resolver.service";
import { taskRoutes } from "./task.routes";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { TaskStatusService } from "./services/task-status.service";
import { TaskCategoryService } from "./services/task-category.service";
import { UpdateTaskComponent } from "./task-update/task-update.component";
import { ToastrService } from "../common/toastr.service";
import { ToastrModule } from 'ngx-toastr';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(taskRoutes),
    ],
    declarations: [
      TaskListComponent,
      TaskThumbnailComponent,
      TaskDetailsComponent,
      CreateTaskComponent,
      UpdateTaskComponent
    ],
    providers: [
      TaskService,
      TaskStatusService,
      TaskCategoryService,
      TaskRouteActivator,
      ToastrService,
      TaskListResolver,
      {
        provide: 'canDeactivateCreateTask',
        useValue: checkDirtyState 
      }
    ]
  })
  export class TaskModule {

  }

  export function checkDirtyState(component: CreateTaskComponent) {
    if (component.taskForm.dirty)
      return component.confirm()
  
      return true
  }
  
