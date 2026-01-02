import {
  Component,
  computed,
  EventEmitter,
  forwardRef,
  Input,
  Output,
  signal,
} from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
} from '@angular/forms';
import { CountryItem } from '@app/types/country-Item';
import { TranslateModule } from '@ngx-translate/core';
import {
  CountryCode,
  getCountries,
  getCountryCallingCode,
  parsePhoneNumberFromString,
} from 'libphonenumber-js';

@Component({
  selector: 'app-phone-number-input',
  imports: [FormsModule, ReactiveFormsModule, TranslateModule],
  templateUrl: './phone-number-input.html',
  styleUrl: './phone-number-input.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PhoneNumberInput),
      multi: true,
    },
  ],
})
export class PhoneNumberInput implements ControlValueAccessor {
  @Input({ required: true }) labelForId!: string;
  @Input() required = true;
  @Output() touchIfEmpty = new EventEmitter<string>();

  // Intl.DisplayNames pour noms pays en français
  private regionNames = new Intl.DisplayNames(['fr'], { type: 'region' });

  // Données pays
  countries: CountryItem[] = getCountries().map((isoCode) => ({
    codeIso: isoCode,
    name: this.regionNames.of(isoCode) ?? isoCode,
    dialCode: `+${getCountryCallingCode(isoCode)}`,
    flagClass: `flag flag-${isoCode.toLowerCase()}`,
    priority: 0,
    htmlId: '',
    placeHolder: '',
  }));

  selectedCountry = signal<CountryItem>(this.countries[0]);
  viewValue = signal('');
  search = signal('');
  dropdownOpen = signal(false);

  // --- Filtrage dynamique ---
  filteredCountries = computed(() => {
    const q = this.search().toLowerCase();
    if (!q) return this.countries;
    return this.countries.filter(
      (c) =>
        c.name.toLowerCase().includes(q) ||
        c.dialCode.includes(q) ||
        c.codeIso.toLowerCase().includes(q),
    );
  });

  // ControlValueAccessor callbacks
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  private onChange: (value: string | null) => void = () => {};
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  private onTouched: () => void = () => {};

  // CVA: Ecrire dans le composant
  writeValue(value: string): void {
    this.viewValue.set(value ?? '');
  }

  // --- CVA: enregistrer les callbacks ---
  registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  // --- Interaction utilisateur ---
  toggleDropdown(): void {
    this.dropdownOpen.update((v) => !v);
  }

  selectCountry(c: CountryItem): void {
    this.selectedCountry.set(c);
    this.dropdownOpen.set(false);
    this.formatAndEmit(this.viewValue());
  }

  onInput(value: string): void {
    this.viewValue.set(value);
    this.formatAndEmit(value);
  }

  private formatAndEmit(value: string): void {
    const country = this.selectedCountry().codeIso as CountryCode;
    const parsedNumber = parsePhoneNumberFromString(value, country);
    if (parsedNumber && parsedNumber.isValid()) {
      this.onChange(parsedNumber.number); //E.164
    } else {
      this.onChange(value);
    }
  }
}
