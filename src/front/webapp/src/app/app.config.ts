import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, LOCALE_ID, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter, TitleStrategy, withComponentInputBinding } from '@angular/router';
import { provideToastr } from 'ngx-toastr';
import { routes } from './app.routes';
import { provideTranslations } from './config/provideTranslations';
import { CustomTitleStrategy } from './core/title/custom-title-strategy';
import { ConfigService } from './services/config.service';
import { APP_BASE_URL } from './services/nswag/api-nswag-client';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(),
    provideRouter(routes, withComponentInputBinding()),
    {
      provide: APP_BASE_URL,
      useFactory: (configService: ConfigService) => configService.getConfig().apiUrl,
      deps: [ConfigService],
    },

    provideTranslations(),
    provideAnimations(),
    provideToastr(),
    { provide: LOCALE_ID, useValue: 'fr-FR' },
    {
      provide: TitleStrategy,
      useClass: CustomTitleStrategy,
    },
  ],
};
