
import { CreateTaskComponent } from "./task-create/create-task.component";
import { TaskDetailsComponent, TaskRouteActivator } from "./task-details";
import { TaskListResolver } from "./task-list/task-list-resolver.service";
import { TaskListComponent } from "./task-list/task-list.component";
import { UpdateTaskComponent } from "./task-update/task-update.component";

export const taskRoutes = [
    { path: 'new', component: CreateTaskComponent, canDeactivate: ['canDeactivateCreateTask'] },
    { path: '', component: TaskListComponent, resolve: { tasks: TaskListResolver } },
    { path: ':taskId', component: UpdateTaskComponent, canActivate: [TaskRouteActivator] },
]