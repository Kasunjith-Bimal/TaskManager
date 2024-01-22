import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Task } from 'src/app/models/task';
import { TaskService } from 'src/app/services/task.service';


@Component({
  selector: 'app-task-list-item',
  templateUrl: './task-list-item.component.html',
  styleUrls: ['./task-list-item.component.css']
})
export class TaskListItemComponent {
  @Output() deleteTaskEvent = new EventEmitter<number>();
  @Input()
  taskdetail?: Task;
  constructor(private taskService: TaskService,private toastr: ToastrService,private router:Router) {
  }

  deleteTask(taskId : number){
    if(confirm("Are you sure to delete")) {
      this.taskService.deleteTask(taskId).subscribe(
        (response: any) => {
          console.log(response);
          if(response.succeeded){
            this.deleteTaskEvent.emit(taskId);
            this.toastr.success('Task deleted successfully', 'Success');
         

          }else{
            this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
          }
        
        },
        error => {
          this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
        }
      );
    }
  }

  editTask(taskId: number){
    this.router.navigate(['/tasks/'+taskId+'/edit']);
  }

  editInLine(taskId: number){

  }
}
