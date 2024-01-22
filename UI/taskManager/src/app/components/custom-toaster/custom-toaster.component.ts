import { Component, OnInit } from '@angular/core';
import { CustomToasterService } from '../../services/custom-toaster.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-custom-toaster',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './custom-toaster.component.html',
  styleUrl: './custom-toaster.component.css',
  providers: [CustomToasterService]
})
export class CustomToasterComponent implements OnInit {
  ngOnInit() {
    this.customToasterService.toasterSubject.subscribe((toast) => {
      console.log(toast);
      this.message = toast.message;
      this.isVisible = true;

      setTimeout(() => {
        this.isVisible = false;
      }, 3000); // Toast duration in milliseconds
    });
  }

  closeToaster() {
    this.isVisible = false;
  }
  isVisible = false;
  message: string  ="";


  constructor(private customToasterService: CustomToasterService) {
    
  }

}
