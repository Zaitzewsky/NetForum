import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/index';
import { LoginComponent } from './components/index';
import { RegistrationComponent } from './components/index';
import { AuthGuard } from './guards/index';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegistrationComponent },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const Routing = RouterModule.forRoot(appRoutes);
