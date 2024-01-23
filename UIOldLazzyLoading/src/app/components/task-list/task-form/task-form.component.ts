import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Task } from 'src/app/models/task';
import { TaskService } from 'src/app/services/task.service';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {
  public isCreateTask : boolean = true;
  isLoading : boolean = false;
  taskForm!: FormGroup;
  taskId : number = 0;
  public task: Task = {
    id: 0, 
    title: '', 
    description: '', 
    dueDate: new Date()
  };
  
  
  constructor(private route: ActivatedRoute,private taskService : TaskService,private toastr: ToastrService,private router:Router,private formbuilder: FormBuilder) {
    
    this.taskForm = this.formbuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      dueDate: ['', Validators.required],
    });
  }

  

 
  ngOnInit(): void {
    
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isLoading = true;
        this.isCreateTask = false;
        this.taskId = Number(params['id'])
        this.loadtaskData(this.taskId);
      }else{
        this.isCreateTask = true; 
        let taskDate =  new Date(this.task.dueDate);
        this.taskForm.setValue({
          title: this.task.title,
          description: this.task.description,
          dueDate: taskDate.toISOString().split('T')[0]
        });
      }
    });
  }

  loadtaskData(taskId : number){
    this.taskService.getTaskById(taskId).subscribe(
      
      (response:any) => {
        if(response.succeeded){
        this.task = response.payload.task;
        console.log(this.task)
        let taskDate : string =  this.task.dueDate.toString();
       
        console.log( taskDate)
        this.taskForm.setValue({
          title: this.task.title,
          description: this.task.description,
          dueDate: taskDate.split('T')[0]
        });
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
  }

  onSubmit(){
   if(this.taskForm.valid){
    let task = this.taskForm.value;
   
    if(this.isCreateTask){
      console.log(task);
      this.addTask(task);
    }else{
      task.id = this.taskId;
      this.editTask(task);
    }
   }else{
    this.toastr.error("Task Form Invalid", 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
   }
  }


  addTask(task: Task){
    this.taskService.addTask(task).subscribe(
      (response: any) => {
        if(response.succeeded){
          this.toastr.success('Task created successfully', 'Success');
          console.log('Task created successfully');
          setTimeout(() => {
            this.router.navigate(['tasks']);
          }, 500);
        }else{
          this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
          setTimeout(() => {
            this.router.navigate(['tasks']);
          }, 500);
        }
       
      },
      error => {
        this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
        setTimeout(() => {
         this.router.navigate(['tasks']);
        }, 500);
      }
    );
  }

  editTask(task: Task){
    console.log(task);
    this.taskService.updateTask(task).subscribe(
      (response: any) => {
        if(response.succeeded){
          this.toastr.success('Task updated successfully', 'Success');
          console.log('Task updated successfully');
          setTimeout(() => {
            this.router.navigate(['tasks']);
          }, 500);
        }else{
          this.toastr.error(response.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
          setTimeout(() => {
            this.router.navigate(['tasks']);
          }, 500);
        }
       
      },
      error => {
        this.toastr.error(error.error.message, 'System Error. Please contact administrator',{timeOut: 3000,extendedTimeOut: 0});
        setTimeout(() => {
         this.router.navigate(['tasks']);
        }, 500);
      }
    );
  }

  
}
