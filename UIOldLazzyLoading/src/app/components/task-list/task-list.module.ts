import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TaskFormComponent } from './task-form/task-form.component';
import { TaskListComponent } from './task-list.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';
import { TaskListItemComponent } from './task-list-item/task-list-item.component';
import { CommonModule } from '@angular/common';
import { SearchPipe } from 'src/app/pipe/search.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component: TaskListComponent },
  { path: ':id/moredetails', component: TaskDetailComponent },
  { path: ':id/edit', component: TaskFormComponent },
  { path: 'add', component: TaskFormComponent },
];

@NgModule({
  declarations: [
    SearchPipe,
    TaskListComponent,
    TaskListItemComponent,
    TaskDetailComponent,
    TaskFormComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule
  ],
})
export class TasksModule {}