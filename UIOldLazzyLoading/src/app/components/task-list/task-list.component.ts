import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Task } from 'src/app/models/task';
import { InteractionService } from 'src/app/services/interaction.service';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  searchText : string = "";
  tasks : Task[] = [];
 
 constructor(private taskService: TaskService,private toastr: ToastrService,private interactionService: InteractionService) {
 }
 ngOnInit(): void {

  this.taskService.getAllTask().subscribe((response:any) => {
     console.log("response",response);
     if(response.succeeded){
      this.tasks = response.payload.tasks;
      this.interactionService.sendTaskCount( this.tasks.length);
     }else{
      this.tasks = [];
      this.interactionService.sendTaskCount(0);
      this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
     }

  },
  error => {
    this.tasks = [];
    this.interactionService.sendTaskCount(0);
    this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 2000,extendedTimeOut: 0});
  }
  );
}

deleteTaskEventClick(taskId : number){
  this.tasks = this.tasks.filter(obj => obj.id !== taskId);
  this.interactionService.sendTaskCount(this.tasks.length);
}

AddTask(){
  
}

  
}
