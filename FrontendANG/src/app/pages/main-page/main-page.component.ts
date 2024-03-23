import { Component, OnInit } from '@angular/core';
import { WeatherForecastService } from '../../shared/services/weather-forecast.service';

@Component({
	selector: 'app-main-page',
	templateUrl: './main-page.component.html',
	styleUrls: [ './main-page.component.css' ],
})
export class MainPageComponent {
	// ngOnInit(): void {
	// 	this.service.getData().subscribe((data) => {
	// 		console.log(data);
	// 	});
	// }

	constructor (private service: WeatherForecastService) { }
}
