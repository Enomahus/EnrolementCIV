import { Directive, inject, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { map, take, tap } from 'rxjs';
import { AppPermission } from '../nswag/api-nswag-client';
import { AuthService } from './auth.service';

@Directive({
  selector: '[appHasPermission]',
})
export class PermissionDirective {
  private readonly templateRef = inject(TemplateRef<unknown>);
  private readonly viewContainer = inject(ViewContainerRef);
  private readonly authService = inject(AuthService);

  @Input() set appHasPermission(permission: AppPermission | AppPermission[] | undefined) {
    if (!permission) {
      this.viewContainer.createEmbeddedView(this.templateRef);
      return;
    }
    let permissionArray = permission;
    if (typeof permissionArray == 'string') {
      permissionArray = [permissionArray];
    }

    this.authService
      .getPermissions()
      .pipe(
        take(1),
        map((perms) =>
          perms.some((p1) => permissionArray.some((p2) => p1.localeCompare(p2) === 0)),
        ),
        tap((hasperm) => {
          if (hasperm) {
            this.viewContainer.createEmbeddedView(this.templateRef);
          } else {
            this.viewContainer.clear();
          }
        }),
      )
      .subscribe();
  }
}
