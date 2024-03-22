import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class WeatherForecastService {
	constructor(private http: HttpClient) {}

	getData():Observable<any> {
		return this.http.get('https://localhost:7278/WeatherForecast');
	}
}
