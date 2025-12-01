import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Language } from '@app/enums/language.enum';
import { LanguageService } from '@app/services/language.service';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-language-switcher',
  imports: [CommonModule, AsyncPipe],
  templateUrl: './language-switcher.html',
  styleUrl: './language-switcher.scss',
})
export class LanguageSwitcher {
  private readonly languageService = inject(LanguageService);
  private readonly translateService = inject(TranslateService);
  langItems: { code: Language; displayText: string; flag: string }[];
  currentLang$: Observable<Language>;

  constructor() {
    // Voluntarily un-translated texts
    this.langItems = [
      { code: Language.fr, displayText: 'Français', flag: 'FR' },
      { code: Language.en, displayText: 'English', flag: 'GB' },
    ];
    this.currentLang$ = this.languageService.getCurrentLanguage();
  }

  onLangChange(lang: Language): void {
    this.languageService.changeLanguage(lang);
  }
}
