import { inject, Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  GuardResult,
  MaybeAsync,
  Router,
} from '@angular/router';
import { map, switchMap, tap } from 'rxjs';
import { AppPermission } from '../nswag/api-nswag-client';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class PermissionsGuard implements CanActivate {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  canActivate(route: ActivatedRouteSnapshot): MaybeAsync<GuardResult> {
    let requiredPermission = route.data['permission'] as
      | AppPermission
      | AppPermission[]
      | undefined;

    if (!(requiredPermission instanceof Array)) {
      requiredPermission = [requiredPermission] as AppPermission[];
    }

    const canActivate$ = this.authService.isAuthenticated().pipe(
      tap((isAuthenticate) => {
        if (!isAuthenticate) {
          this.router.navigate(['/login'], { queryParams: { state: route.url.join('/') } });
        }
      }),
      map(() => true),
    );

    if (!requiredPermission) return canActivate$;

    return canActivate$.pipe(
      switchMap(() => this.authService.getPermissions()),
      map((perms) => perms.some((p) => requiredPermission.includes(p))),
      tap((hasPerm) => {
        if (!hasPerm) {
          this.router.navigate(['/login']);
        }
      }),
      //map(([hasPerm]) => hasPerm)
    );
  }
}
