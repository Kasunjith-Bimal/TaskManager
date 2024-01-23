import { DatePipe } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Task } from 'src/app/models/task';
import { TaskService } from './../../../services/task.service';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-task-add',
  templateUrl: './task-add.component.html',
  styleUrls: ['./task-add.component.css']
})
export class TaskAddComponent implements OnInit {
  /**
   *
   */
  constructor(private route: ActivatedRoute,private taskService : TaskService,private toastr: ToastrService,private router:Router) {

  }
  @Output() addedTaskEvent = new EventEmitter<Task>();
  currentDate: any ='';
  ngOnInit(): void {
   
  }

  

  addTask(task: Task){
    // let taskDate : string =  this.task.dueDate.toString();
    // console.log("TaskDate",taskDate);
    this.taskService.addTask(task).subscribe(
      (response: any) => {
        if(response.succeeded){
          this.toastr.success('Task created successfully', 'Success');
          console.log('Task created successfully');
          console.log('Response');
          this.addedTaskEvent.emit(response.payload.task);
           this.task ={
            id: 0, 
            title: '', 
            description: '', 
            dueDate: new Date()
          };
          

        }else{
          this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
        }
       
      },
      error => {
        this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
      }
    );
  }
  public task: Task = {
    id: 0, 
    title: '', 
    description: '', 
    dueDate: new Date()
  };

}
