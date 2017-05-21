import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { User }      from '../../models/user';

import { AlertService, RegisterService } from '../../services/index';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  title = 'Register';
  user = new User();
  loading = false;

  constructor(
    private router: Router,
    private registerService: RegisterService,
    private alertService: AlertService) { }

  onSubmit(){
    this.register();
  }

  register() {
  this.loading = true;
  this.registerService.register(this.user)
      .subscribe(
          data => {
              this.alertService.success('Registration successful', true);
              this.router.navigate(['/login']);
              this.loading = false;
          },
          error => {
              var errorMessageBody = JSON.parse(error._body);
              this.alertService.error(errorMessageBody.message);
              this.loading = false;
          });
}
}
