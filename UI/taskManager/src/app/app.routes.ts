import { Routes } from '@angular/router';
import { TaskDetailComponent } from './components/task-list/task-detail/task-detail.component';
import { TaskListComponent } from './components/task-list/task-list.component';


export const routes: Routes = [
   {
    path: '',
    pathMatch: 'full',
    redirectTo: 'tasks'
   },
   { path: 'tasks', component: TaskListComponent},
   { path: 'tasks/:id/moredetails', component: TaskDetailComponent },
//    {path: 'tasks', loadComponent: () => import('./components/task-list/task-list.component').then(mod => mod.TaskListComponent)},
//    {path: 'tasks/:id/moredetails', loadComponent: () => import('./components/task-list/task-detail/task-detail.component').then(mod => mod.TaskDetailComponent)},
   
];
