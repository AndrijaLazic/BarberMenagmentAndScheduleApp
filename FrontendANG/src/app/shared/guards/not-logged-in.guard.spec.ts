import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { NotLoggedInGuard } from './not-logged-in.guard';

describe('NotLoggedInGuard', () => {
	const executeGuard: CanActivateFn = (...guardParameters) =>
		TestBed.runInInjectionContext(() => NotLoggedInGuard(...guardParameters));

	beforeEach(() => {
		TestBed.configureTestingModule({});
	});

	it('should be created', () => {
		expect(executeGuard).toBeTruthy();
	});
});
