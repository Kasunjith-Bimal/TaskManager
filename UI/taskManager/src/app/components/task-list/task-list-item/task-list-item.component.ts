import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { Task } from '../../../models/task';
import { CommonModule, DatePipe } from '@angular/common';
import { TaskService } from '../../../services/task.service';
import { ToastrService } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { Router } from 'express';


@Component({
  selector: 'app-task-list-item',
  standalone: true,
  imports: [DatePipe,CommonModule,RouterModule],
  templateUrl: './task-list-item.component.html',
  styleUrl: './task-list-item.component.css',
  providers:[TaskService]
})
export class TaskListItemComponent implements OnInit {
  @Output() deleteTaskEvent = new EventEmitter<number>();
  @Input()
  taskdetail?: Task;
  toster = inject(ToastrService);
  constructor(private taskService: TaskService) {
  }
  ngOnInit(): void {
    this.toster.clear();
  }
  editTask(taskId : number ){

  }

 

  deleteTask(taskId : number){
    const toastrWarning = this.toster.warning(
      'Are you sure you want to delete this task?',
      'Confirmation',
      {
        timeOut: 0,
        extendedTimeOut: 0,
        closeButton: true,
        positionClass: 'toast-top-center',
      }
    );

    toastrWarning.onTap.subscribe(() => {
      this.taskService.deleteTask(taskId).subscribe(
        (response: any) => {
          console.log(response);
          if(response.succeeded){
            this.deleteTaskEvent.emit(taskId);
            this.toster.success('Task deleted successfully', 'Success');
         

          }else{
            this.toster.error(response.message, 'Error',{timeOut: 0,extendedTimeOut: 0});
          }
        
        },
        error => {
          this.toster.error(error.error.message, 'Error',{timeOut: 0,extendedTimeOut: 0});
        }
      );
    });
  }
}
