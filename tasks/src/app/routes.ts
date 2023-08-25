import { Routes } from '@angular/router'
import { Error404Component } from './errors/404.component';
import { AuthGuard } from './user/auth.guard';


export const appRoutes:Routes = [
    
    { path: '404', component: Error404Component },
    { path: '', redirectTo: '/tasks', pathMatch: 'full' },
    { 
        path: 'tasks', 
        canActivateChild: [AuthGuard],
        loadChildren: () => import('./tasks/task.module')
            .then(module => module.TaskModule)
    },
    { 
        path: 'logs', 
        canActivateChild: [AuthGuard],
        loadChildren: () => import('./tasks/logs/logs.module')
            .then(module => module.LogsModule)
    },
    { 
        path: 'user', 
        canActivateChild: [AuthGuard],
        loadChildren: () => import('./user/user.module')
            .then(module => module.UserModule)
    }

]