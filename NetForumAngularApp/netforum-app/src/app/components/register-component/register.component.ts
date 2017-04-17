import { Component } from '@angular/core';
import { User }      from '../../models/user';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
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
