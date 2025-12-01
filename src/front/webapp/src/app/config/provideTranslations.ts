import { HttpClient } from '@angular/common/http';
import { EnvironmentProviders, importProvidersFrom, inject } from '@angular/core';
import { TranslateLoader, TranslateModule, TranslationObject } from '@ngx-translate/core';
import { catchError, forkJoin, map, Observable, of } from 'rxjs';
import packageJson from '../../../package.json';

class CreateTranslationLoader implements TranslateLoader {
  private readonly http = inject(HttpClient);

  getTranslation(lang: string): Observable<TranslationObject> {
    const baseI18nDirectory = '/assets/i18n';
    const directories = [baseI18nDirectory];
    const tasks$ = directories.map((d) => {
      const filename = `${d}/${lang}.json?v=${packageJson.version}`;
      const fallback = `${d}/en.json?v=${packageJson.version}`;

      return this.http.get<TranslationObject>(filename).pipe(
        catchError((err) => {
          console.warn(`Fichier de traduction manquant : ${filename}`, err);
          return this.http.get<TranslationObject>(fallback).pipe(
            catchError(() => {
              console.error(`Fichier de fallback manquant : ${fallback} `);
              return of({});
            }),
          );
        }),
      );
    });

    return forkJoin(tasks$).pipe(
      map((translates) => {
        return translates.reduce((prev, curr) => {
          return { ...prev, ...curr };
        }, {});
      }),
    );
  }
}

export const provideTranslations = (): EnvironmentProviders =>
  importProvidersFrom(
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: CreateTranslationLoader,
        deps: [HttpClient],
      },
    }),
  );
