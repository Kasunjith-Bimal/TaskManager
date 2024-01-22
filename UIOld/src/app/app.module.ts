import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchPipe } from './pipe/search.pipe';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TaskService } from './services/task.service';
import { NavigationBarComponent } from './components/navigation-bar/navigation-bar.component';
import { TaskFormComponent } from './components/task-form/task-form.component';
import { TaskListComponent } from './components/task-list/task-list.component';
import { TaskDetailComponent } from './components/task-list/task-detail/task-detail.component';
import { TaskListItemComponent } from './components/task-list/task-list-item/task-list-item.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InteractionService } from './services/interaction.service';
@NgModule({
  declarations: [
    AppComponent,
    SearchPipe,
    NavigationBarComponent,
    TaskFormComponent,
    TaskListComponent,
    TaskDetailComponent,
    TaskListItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      positionClass: 'toast-top-right',
      preventDuplicates: false,
      closeButton: false,
    }),
    ReactiveFormsModule,
    
  ],
  providers: [TaskService,InteractionService],
  bootstrap: [AppComponent]
})
export class AppModule { }
