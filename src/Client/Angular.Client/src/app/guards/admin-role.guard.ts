import { CanActivateFn } from '@angular/router';
import {Inject} from "@angular/core";
import {AuthService} from "../services/auth.service";

export const adminRoleGuard: CanActivateFn = (route, state) => {
  const authService = Inject(AuthService);

  if (authService.isLogged()) {
    const roles = authService.getUserRoles();
    console.log(roles);

    if (Array.isArray(roles)) {
      return roles.includes('Admin');
    } else {
      return roles === 'Admin';
    }
  }

  return false;
};
