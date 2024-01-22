import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NavigationBarComponent } from "./components/navigation-bar/navigation-bar.component";
// import { ToastrService } from 'ngx-toastr';


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, NavigationBarComponent]
})
export class AppComponent {
  title = 'taskManager';
  // toster = inject(ToastrService)
  // some(){
  //   this.toster.success("sss","sucess");
  // }
}
