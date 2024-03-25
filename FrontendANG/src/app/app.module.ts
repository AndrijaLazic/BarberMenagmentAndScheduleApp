import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { NavigationBarComponent } from './shared/components/navigation-bar/navigation-bar.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { NgToastModule } from "ng-angular-popup";

@NgModule({
	declarations: [ AppComponent, MainPageComponent ],
	imports: [
		BrowserModule,
		AppRoutingModule,
		HttpClientModule,
		NavigationBarComponent,
		FooterComponent,
		NgToastModule
	],
	providers: [],
	bootstrap: [ AppComponent ],
})
export class AppModule { }
