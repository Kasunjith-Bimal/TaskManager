import { Component, Inject, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../../../services/task.service';
import { Task } from '../../../models/task';
import { CommonModule, DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { CustomToasterComponent } from '../../custom-toaster/custom-toaster.component';


@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule,DatePipe],
  templateUrl: './task-detail.component.html',
  styleUrl: './task-detail.component.css',
  providers:[TaskService]
})
export class TaskDetailComponent implements OnInit {

  constructor(private route: ActivatedRoute,private taskService : TaskService ) {

  }
  public task : Task | undefined;
  toster = inject(ToastrService);
  ngOnInit(): void {
    this.toster.clear();
    this.route.paramMap.subscribe(params => {
      this.toster.clear();
     let taskId = Number(params.get("id"));
     console.log(taskId);
     this.taskService.getTaskById(taskId).subscribe((response:any) => {
      if(response.succeeded){
       this.task = response.payload.task;
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
