import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Task } from 'src/app/models/task';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent implements OnInit {
  public task : Task | undefined;
  isLoading : boolean = false
  constructor(private route: ActivatedRoute,private taskService : TaskService,private toastr: ToastrService,private router:Router ) {

  }
  ngOnInit(): void {
    
    this.route.paramMap.subscribe(params => {
    this.isLoading = true;
     let taskId = Number(params.get("id"));
     console.log(taskId);
     this.taskService.getTaskById(taskId).subscribe((response:any) => {
      if(response.succeeded){
       this.task = response.payload.task;
       this.isLoading = false;
      }else{
        this.isLoading = false;
         this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
         setTimeout(() => {
          this.router.navigate(['tasks']);
         }, 500);
        
      }
 
      },
      error => {
        this.isLoading = false;
         this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
         setTimeout(() => {
          this.router.navigate(['tasks']);
         }, 500);
      }
     );
     
    });
   
  }
 

  

}
