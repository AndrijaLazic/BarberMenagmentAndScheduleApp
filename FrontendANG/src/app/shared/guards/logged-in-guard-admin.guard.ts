import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AdminAuthService } from '../services/admin/admin-auth.service';

export const loggedInGuardAdminGuard: CanActivateFn = () => {
	const authService=inject(AdminAuthService);
	return authService.isLoggedIn();
};
