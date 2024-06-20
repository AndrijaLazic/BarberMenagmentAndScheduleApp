import { Component } from '@angular/core';
import { FooterComponent } from './shared/components/footer/footer.component';
import { NgToastModule } from 'ng-angular-popup';
import { RouterOutlet } from '@angular/router';
import { NavigationBarComponent } from './shared/components/navigation-bar/navigation-bar.component';
import { CommonModule } from '@angular/common';
@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: [ './app.component.css' ],
	standalone: true,
	imports: [
		CommonModule,
		NavigationBarComponent,
		RouterOutlet,
		NgToastModule,
		FooterComponent,
	],
})
export class AppComponent {
	title = 'FrontendANG';
}
