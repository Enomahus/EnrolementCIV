import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { LanguageService } from '@app/services/language.service';
import { APP_BASE_URL } from '@app/services/nswag/api-nswag-client';
import { combineLatest, Observable, switchMap, take } from 'rxjs';

export function langInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> {
  const baseApiUrl = inject(APP_BASE_URL);
  if (!req.url.startsWith(baseApiUrl)) {
    return next(req);
  }

  const langService = inject(LanguageService);
  return combineLatest([langService.getCurrentLanguage(), langService.getCurrentLocale()]).pipe(
    take(1),
    switchMap(([lang, locale]) => {
      if (lang) {
        const headers = req.headers.append(
          'Accept-Language',
          `${locale},${lang};q=0.9,en-US,en;q=0.8`,
        );
        req = req.clone({
          headers,
        });
      }

      return next(req);
    }),
  );
}
