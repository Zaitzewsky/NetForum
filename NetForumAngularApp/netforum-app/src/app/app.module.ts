import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './routing/app-routing.module';

import { AppComponent } from './components/app-component/app.component';
import { RegisterComponent } from './components/register-component/register.component';

import { AlertComponent } from './directives/alert.component';
import { AuthGuard } from './guards/auth.guard';

import { AlertService, AuthenticationService, RegisterService } from './services/index';

@NgModule({
  declarations: [
    RegisterComponent,
    AppComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule
  ],
  providers: [
    AuthGuard,
    AlertService,
    AuthenticationService,
    RegisterService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
