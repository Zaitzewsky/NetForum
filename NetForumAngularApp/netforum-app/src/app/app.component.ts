import { Component } from '@angular/core';

import { User }    from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'User page';
  user = new User('Jacinto', 'Micales', 'Jazz', 'Backflip180', '07396602569', 'micales@hotmail.se');
  submitted = false;

  onsubmit(){
    this.submitted = true;
  }

  get diagnostic(){ 
    return JSON.stringify(this.user); 
  }
}
