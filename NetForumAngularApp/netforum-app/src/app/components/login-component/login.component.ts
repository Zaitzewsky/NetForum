import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AlertService, AuthenticationService } from '../../services/index';
import { User } from '../../models/user';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    user = new User();
    loading = false;
    returnUrl: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService) { }

    ngOnInit() {
        this.authenticationService.logout();
    }

    login() {
        this.loading = true;
        this.authenticationService.login(this.user.username, this.user.password)
            .subscribe(
                data => {
                    this.alertService.success('Login successful', true);
                    this.router.navigate(['/register']);
                },
                error => {
                    var errorMessageBody = JSON.parse(error._body);
                    this.alertService.error(errorMessageBody.error_description);
                    this.loading = false;
                });
    }
}
