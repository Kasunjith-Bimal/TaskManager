import { Component, OnInit } from '@angular/core';
import { InteractionService } from './../../services/interaction.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
  constructor(private interactionService : InteractionService){

  }
  taskCount : number = 0;
 ngOnInit(): void {
   
   this.interactionService.tasksCount.subscribe((count=>{
     this.taskCount = count;
  }))
 }
 
}
