import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InteractionService {
  public tasksCount: BehaviorSubject<number> = new BehaviorSubject(0);
  constructor() { }

  sendTaskCount(count: number){
    this.tasksCount.next(count);
  }
}
