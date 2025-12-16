import { Component, EventEmitter, inject, OnInit, Output, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CountryService } from '@app/services/api/country-api.service';
import { GetCountriesResponse } from '@app/services/nswag/api-nswag-client';
import { TranslateModule } from '@ngx-translate/core';

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

  private readonly countriesService = inject(CountryService);
  selectedDialCode = signal<string>('+225');
  localNumber = signal<string>('');
  countries = signal<GetCountriesResponse[]>([]);

  @Output() phoneChange = new EventEmitter<string>();

  ngOnInit(): void {
    this.countriesService.getCountries().subscribe((res) => this.countries.set(res));
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
