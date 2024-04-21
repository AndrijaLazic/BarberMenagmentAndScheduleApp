import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { NotLoggedInGuard } from './shared/guards/not-logged-in.guard';
import { MessagingPageComponent } from './pages/messaging-page/messaging-page.component';
import { LoggedInGuard } from './shared/guards/logged-in.guard';
import { AdminPageComponent } from './pages/admin-page/admin-page.component';
import { AdminAuthService } from './shared/services/admin/admin-auth.service';
import { AuthService } from './shared/services/auth.service';


const routes: Routes = [
	{ path: 'registracija', component: RegisterPageComponent, canActivate:[ NotLoggedInGuard ]},
	{ path: 'prijava', component: LoginPageComponent, canActivate:[ NotLoggedInGuard ], providers:[ {provide: 'IAuthService', useClass: AuthService} ] },
	{ path: 'poruke', component: MessagingPageComponent, canActivate:[ LoggedInGuard ] },
	{ path: 'admin', children:[
		{ path: 'prijava', component: LoginPageComponent, providers:[ {provide: 'IAuthService', useClass: AdminAuthService} ]},
		{ path: 'panel', component: AdminPageComponent},
		{ path: '', redirectTo:"prijava", pathMatch:"full" },
	] },

];

@NgModule({
	imports: [ RouterModule.forRoot(routes) ],
	exports: [ RouterModule ],
})
export class AppRoutingModule { }
