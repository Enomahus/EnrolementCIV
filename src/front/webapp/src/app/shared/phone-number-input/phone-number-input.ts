import {
  Component,
  computed,
  effect,
  EventEmitter,
  inject,
  Input,
  OnInit,
  Output,
  signal,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CountryService } from '@app/services/api/country-api.service';
import { GetCountriesResponse } from '@app/services/nswag/api-nswag-client';
import { CountryItem } from '@app/types/country-Item';
import { TranslateModule } from '@ngx-translate/core';
import parsePhoneNumberFromString, {
  AsYouType,
  CountryCode,
  getCountries,
  getCountryCallingCode,
} from 'libphonenumber-js/max';

@Component({
  selector: 'app-phone-number-input',
  imports: [FormsModule, ReactiveFormsModule, TranslateModule],
  templateUrl: './phone-number-input.html',
  styleUrl: './phone-number-input.scss',
})
export class PhoneNumberInput implements OnInit {
  // @Input({ required: true }) control!: FormControl;
  // @Input({ required: true }) labelForId!: string;
  // @Input() readonly = false;

  /** Pays initial (ISO2) */
  @Input() initialCountry = 'CI';
  /** Pays favoris épinglés en tête de liste */
  @Input() preferredCountries: string[] = ['FR', 'CI', 'GB', 'US'];
  /** Emission de la valeur en E.164 (ex: +33123456789) */
  @Output() phoneChange = new EventEmitter<string | null>();

  // État UI
  showDropdown = signal(false);
  search = signal('');
  selectedIso2 = signal(this.initialCountry);
  nationalInput = signal(''); // seulement les chiffres saisis côté national
  formattedNational = signal(''); // rendu formaté "au fil de la frappe"

  // Intl.DisplayNames pour noms pays en français
  private regionNames = new Intl.DisplayNames(['fr'], { type: 'region' });

  // Construire la liste des pays depuis libphonenumber-js
  private allCountries: CountryItem[] = getCountries().map((iso2) => ({
    codeIso: iso2,
    name: this.regionNames.of(iso2) ?? iso2,
    dialCode: `+${getCountryCallingCode(iso2)}`,
    flagClass: `fi fi-${iso2.toLowerCase()}`,
  }));

  // Tri: favoris en tête puis alphabétique
  private sortedCountries = computed(() => {
    const prefSet = new Set(this.preferredCountries.map((c) => c.toUpperCase()));
    const head = this.allCountries.filter((c) => prefSet.has(c.codeIso!.toUpperCase()));
    const tail = this.allCountries.filter((c) => !prefSet.has(c.codeIso!.toUpperCase()));
    head.sort((a, b) => a.name!.localeCompare(b.name!));
    tail.sort((a, b) => a.name!.localeCompare(b.name!));
    return [...head, ...tail];
  });

  // Filtre de recherche
  filteredCountries = computed(() => {
    const q = this.search().trim().toLowerCase();
    if (!q) return this.sortedCountries();
    return this.sortedCountries().filter(
      (c) =>
        c.name!.toLowerCase().includes(q) ||
        c.codeIso!.toLowerCase().includes(q) ||
        c.dialCode!.replace('+', '').includes(q),
    );
  });

  // Formateur "AsYouType" lié au pays sélectionné
  private formatter = new AsYouType(this.selectedIso2() as CountryCode);

  constructor() {
    // Re-créer le formateur si le pays change
    effect(() => {
      const iso = this.selectedIso2();
      this.formatter = new AsYouType(iso as CountryCode);
      // reformatter l'entrée actuelle
      const digits = this.nationalInput();
      this.formattedNational.set(this.formatter.input(digits));
      this.emitE164();
    });
  }

  /** Saisie clavier côté input national */
  onInputChange(event: Event): void {
    const raw = (event.target as HTMLInputElement).value;
    // Autoriser chiffres, espace, -, (, ), mais normaliser vers digits pour "input"
    const onlyDigits = raw.replace(/[^\d]/g, '');
    this.nationalInput.set(onlyDigits);
    this.formattedNational.set(this.formatter.input(onlyDigits));
    this.emitE164();
  }

  /** Sélection d’un pays dans le menu */
  selectCountry(c: GetCountriesResponse): void {
    this.selectedIso2.set(c.codeIso!);
    this.showDropdown.set(false);
  }

  /** Emettre la valeur en E.164 (+CCNN...) si valide ; sinon null */
  private emitE164(): void {
    const iso = this.selectedIso2() as CountryCode;
    const dial = getCountryCallingCode(iso);
    // Concaténer indicatif + nationalInput
    const intlCandidate = `+${dial}${this.nationalInput()}`;
    const parsed = parsePhoneNumberFromString(intlCandidate, iso);
    const e164 = parsed && parsed.isValid() ? parsed.number : null;
    this.phoneChange.emit(e164);
  }

  // Utils UI
  toggleDropdown(): void {
    this.showDropdown.set(!this.showDropdown());
  }
  closeDropdown(): void {
    this.showDropdown.set(false);
  }

  private readonly countriesService = inject(CountryService);
  selectedDialCode = signal<string>('+225');
  localNumber = signal<string>('');
  countries = signal<GetCountriesResponse[]>([]);

  //@Output() phoneChange = new EventEmitter<string>();

  // public onPhoneChange(): void {
  //   if (!this.control.value || this.control.errors) {
  //     return;
  //   }

  //   const phoneNumber = this.formatPhoneNumber(this.control.value);
  //   this.control.setValue(phoneNumber);
  // }

  // public formatPhoneNumber(phone: string, countryCode: CountryCode): string {
  //   const phoneNumber = parsePhoneNumberWithError(phone, countryCode);
  //   const formattedNumber = phoneNumber.formatInternational();
  //   return formattedNumber;
  // }

  ngOnInit(): void {
    this.countriesService.getCountries().subscribe((res) => {
      console.log(res);
      this.countries.set([...res]);
    });
  }

  onCountryChange(event: Event): void {
    const value = (event.target as HTMLSelectElement).value;
    this.selectedDialCode.set(value);
    this.emitPhone();
  }

  onNumberChange(value: string): void {
    this.localNumber.set(value);
    this.emitPhone();
  }

  private emitPhone(): void {
    const dial = this.selectedDialCode();
    const num = this.localNumber().trim();
    const formatted = num ? `${dial} ${num.replace(/\s+/g, '')}` : '';

    this.phoneChange.emit(formatted);
  }
}
