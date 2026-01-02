import { Component, Input, signal } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { CountryISO, NgxIntlTelInputModule } from 'ngx-intl-tel-input';

export interface ChangeData {
  number?: string;
  internationalNumber?: string;
  nationalNumber?: string;
  e164Number?: string;
  countryCode?: string;
  dialCode?: string;
}

@Component({
  selector: 'app-phone-input-intl',
  imports: [FormsModule, NgxIntlTelInputModule],
  templateUrl: './phone-input-intl.html',
  styleUrl: './phone-input-intl.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: PhoneInputIntl,
      multi: true,
    },
  ],
})
export class PhoneInputIntl implements ControlValueAccessor {
  @Input()
  preferredCountries: CountryISO[] = [CountryISO.CôteDIvoire];

  @Input()
  selectFirstCountry = false;

  @Input()
  maxLength = 15;

  @Input()
  required = true;

  @Input({ required: true }) labelForId!: string;
  @Input() name = signal('phoneNumber');

  selectedCountryISO: CountryISO = CountryISO.CôteDIvoire;
  disabled = signal(false);
  viewValue = signal('');

  // eslint-disable-next-line @typescript-eslint/no-empty-function
  onChange: (value?: string | undefined) => void = () => {};
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  onTouched: () => void = () => {};

  writeValue(value?: string): void {
    this.viewValue.set(value ?? '');
  }

  registerOnChange(fn: (value: string | undefined) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled.set(isDisabled);
  }

  mapChange(arg?: ChangeData): void {
    if (arg && this.viewValue() !== arg?.internationalNumber) {
      this.writeValue(arg?.internationalNumber);
      this.onChange(arg?.internationalNumber);
    }
  }
}
