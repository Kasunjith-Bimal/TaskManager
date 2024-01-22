import { Component, OnInit, inject } from '@angular/core';
import { TaskListItemComponent } from "./task-list-item/task-list-item.component";
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task';
import { response } from 'express';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchPipe } from '../../pipe/search.pipe';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-task-list',
    standalone: true,
    templateUrl: './task-list.component.html',
    styleUrl: './task-list.component.css',
    imports: [TaskListItemComponent,TaskListItemComponent,CommonModule,FormsModule,SearchPipe,HttpClientModule],
providers: [TaskService]
})
export class TaskListComponent implements OnInit {
  searchText : string = "";
  tasks : Task[] = [];
  toster = inject(ToastrService);
  constructor(private taskService: TaskService) {
  }

  ngOnInit(): void {
    this.toster.clear();
    this.taskService.getAllTask().subscribe((response:any) => {
       console.log("response",response);
       if(response.succeeded){
        this.tasks = response.payload.tasks;
       }else{
        this.tasks = [];
       }
  
    },
    error => {
      this.tasks = [];
      console.error('Error deleting task:', error);
    }
    );
  }

  deleteTaskEventClick(deleteTaskId: number){
    console.log("deleteTaskId",deleteTaskId);
   this.tasks = this.tasks.filter(obj => obj.id !== deleteTaskId);
  }

}
