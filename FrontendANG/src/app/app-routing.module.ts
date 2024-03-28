import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { NotLoggedInGuard } from './shared/guards/not-logged-in.guard';


const routes: Routes = [
	{ path: 'registracija', component: RegisterPageComponent, pathMatch: 'full', canActivate:[ NotLoggedInGuard ]},
	{ path: 'prijava', component: LoginPageComponent, pathMatch: 'full', canActivate:[ NotLoggedInGuard ] },
];

@NgModule({
	imports: [ RouterModule.forRoot(routes) ],
	exports: [ RouterModule ],
})
export class AppRoutingModule { }
