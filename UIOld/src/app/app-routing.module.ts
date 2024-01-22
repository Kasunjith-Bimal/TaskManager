import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list.component';
import { TaskDetailComponent } from './components/task-list/task-detail/task-detail.component';
import { TaskFormComponent } from './components/task-form/task-form.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'tasks'
   },
   { path: 'tasks', component: TaskListComponent},
   { path: 'tasks/:id/moredetails', component: TaskDetailComponent },
   { path: 'tasks/:id/edit', component: TaskFormComponent },
   { path: 'tasks/add', component: TaskFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
