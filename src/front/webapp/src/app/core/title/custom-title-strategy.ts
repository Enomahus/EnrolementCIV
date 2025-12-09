import { inject, Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterStateSnapshot, TitleStrategy } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin, switchMap, take } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomTitleStrategy extends TitleStrategy {
  private readonly title = inject(Title);
  private readonly translateService = inject(TranslateService);

  constructor() {
    super();
  }

  override updateTitle(snapshot: RouterStateSnapshot): void {
    const routeTitlekey = this.buildTitle(snapshot);
    const projectTitle$ = this.translateService.get('global.projectTitle').pipe(take(1));

    if (routeTitlekey) {
      const routeTitle$ = this.translateService.get(routeTitlekey).pipe(take(1));
      forkJoin([projectTitle$, routeTitle$])
        .pipe(
          switchMap(([projectTitle, routeTitle]) =>
            this.translateService.get('global.titleTemplate', {
              title: routeTitle,
              project: projectTitle,
            }),
          ),
          take(1),
        )
        .subscribe((finalTitle) => this.title.setTitle(finalTitle));
    } else {
      projectTitle$
        .pipe(
          switchMap((projectTitle) =>
            this.translateService.get('global.noTitleTemplate', {
              project: projectTitle,
            }),
          ),
          take(1),
        )
        .subscribe((fallbackTitle) => this.title.setTitle(fallbackTitle));
    }
  }
}
