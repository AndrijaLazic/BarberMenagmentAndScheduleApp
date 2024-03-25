import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';

const routes: Routes = [
	{ path: '', component: MainPageComponent, pathMatch: 'full' },
	{ path: 'registracija', component: RegisterPageComponent, pathMatch: 'full' },
	{ path: 'prijava', component: LoginPageComponent, pathMatch: 'full' },
];

@NgModule({
	imports: [ RouterModule.forRoot(routes) ],
	exports: [ RouterModule ],
})
export class AppRoutingModule { }
