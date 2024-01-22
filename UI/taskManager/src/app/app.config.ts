import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { PreloadAllModules, provideRouter, withComponentInputBinding, withDebugTracing, withPreloading } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideToastr } from 'ngx-toastr';
import { HttpClientModule, provideHttpClient, withFetch, withJsonpSupport } from '@angular/common/http';
import { TaskService } from './services/task.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomToasterService } from './services/custom-toaster.service';


export const appConfig: ApplicationConfig = {
  // providers: [importProvidersFrom(HttpClientModule),provideHttpClient(), provideClientHydration(),provideToastr(),provideClientHydration()]
  providers: [
    provideRouter(routes,withComponentInputBinding()),
    TaskService,
    CustomToasterService,
    importProvidersFrom(HttpClientModule),
    provideToastr(),
    importProvidersFrom([BrowserAnimationsModule])
  
  ]
};
