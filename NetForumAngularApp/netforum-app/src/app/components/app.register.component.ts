import { Component } from '@angular/core';
import { User }    from '../models/user';

@Component({
  selector: 'app-root',
  templateUrl: '../html/register/register-form.component.html',
  styleUrls: ['../css/app.component.css', '../css/app.component.forms.css']
})

export class RegisterComponent {
  title = 'Register';
  user = new User();
  submitted = false;

  onsubmit(){
    this.submitted = true;
    this.user = null;
  }
}
