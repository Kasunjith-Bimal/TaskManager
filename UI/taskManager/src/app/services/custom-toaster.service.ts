import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomToasterService {

  constructor() { }

  toasterSubject = new Subject<any>();

  showToast(message: string) {
    this.toasterSubject.next({ message });
  }
}
