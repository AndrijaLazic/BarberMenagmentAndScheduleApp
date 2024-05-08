import { ErrorHandler, importProvidersFrom } from '@angular/core';
import { AppComponent } from './app/app.component';
import { NgToastModule } from 'ng-angular-popup';
import { withInterceptorsFromDi, provideHttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app/app-routing.module';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { CustomErrorHandlerService } from './app/shared/services/custom-error-handler.service';
import { globalHttpErrorHandlerInterceptor } from './app/shared/Interceptors/global-http-error-handler.interceptor';
import { globalHttpHeaderInterceptor } from './app/shared/Interceptors/header-interceptor';

bootstrapApplication(AppComponent, {
	providers: [
		importProvidersFrom(
			BrowserModule, AppRoutingModule, NgToastModule
		),
		provideHttpClient(withInterceptorsFromDi()),
		{
			provide:ErrorHandler,
			useClass:CustomErrorHandlerService
		},
		{
			provide:HTTP_INTERCEPTORS,
			useClass:globalHttpErrorHandlerInterceptor,
			multi : true
		},
		{
			provide:HTTP_INTERCEPTORS,
			useClass:globalHttpHeaderInterceptor,
			multi : true
		}
	]
})
	.catch((err) => console.error(err));
