import { CommonModule } from '@angular/common';
import { Component, computed, effect, signal } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { patternEmail, patternPassword } from '@app/constants';
import { PersonTitle } from '@app/services/nswag/api-nswag-client';
import { passwordMatchValidator } from '@app/shared/helpers/for-helper';
import { PhoneNumberInput } from '@app/shared/phone-number-input/phone-number-input';
import { StickyButtonsContainerComponent } from '@app/shared/sticky-buttons-container/sticky-buttons-container.component';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-create-account',
  imports: [
    RouterLink,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    StickyButtonsContainerComponent,
    TranslateModule,
    PhoneNumberInput,
  ],
  templateUrl: './create-account.html',
  styleUrl: './create-account.scss',
})
export class CreateAccount {
  // --- Signals d’UI ---
  showPassword = signal(false);
  showConfirm = signal(false);
  termsAccepted = signal(false);
  submitting = signal(false);

  form: FormGroup;

  constructor() {
    this.form = new FormGroup(
      {
        civility: new FormControl<PersonTitle>(PersonTitle.Mr, {
          validators: [Validators.required],
          nonNullable: true,
        }),

        lastName: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required],
        }),
        firstName: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required],
        }),
        dialCode: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required],
        }),
        phoneNumber: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required],
        }),
        email: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required, Validators.email, Validators.pattern(patternEmail)],
        }),
        password: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required, Validators.pattern(patternPassword)],
        }),
        confirmPassword: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required],
        }),
      },
      [passwordMatchValidator('password', 'confirmPassword')],
    );

    //Optionnel: effect pour le debug
    effect(() => {
      console.log('Form valid: ', this.form.valid);
    });
  }

  onPhoneChange(value: string): void {
    //this.form.controls.phoneNumber.setValue(value);
    this.form.get('phoneNumber')?.setValue(value);
  }

  //Helpers
  ctrl(name: string): AbstractControl | null {
    return this.form.get(name);
  }

  // Marquer le contrôle comme "touched" au blur, pour n’afficher l’erreur que si l’utilisateur a touché le champ.
  touchIfEmpty(name: string): void {
    const c = this.ctrl(name);
    if ((c!.value ?? '') === '') {
      c!.markAsTouched();
      c!.updateValueAndValidity({ onlySelf: true });
    }
  }

  // Afficher erreur "requis" si touched ET valeur vide
  showRequiredError(name: string): boolean {
    const c = this.ctrl(name);
    return c!.touched && (c!.value === '' || c!.value === null) && c!.hasError('required');
  }

  showEmailError(): boolean {
    const c = this.ctrl('email');
    return c!.touched && !!c!.value && c!.hasError('email');
  }

  showPasswordComplexity(): boolean {
    const c = this.ctrl('password');
    return c!.touched && !!c!.value && c!.hasError('passwordComplexity');
  }

  showPasswordsDontMatch(): boolean {
    return this.form.touched && this.form.hasError('passwordsDontMatch');
  }

  // Signal pour le bouton désactivé
  disableSubmit = computed<boolean>(
    () => this.submitting() || !this.form.valid || !this.termsAccepted(),
  );

  submit(): void {
    // Marquer tout touché si besoin (UX)
    Object.values(this.form.controls).forEach((c) => c.markAsTouched());

    if (this.form.invalid || !this.termsAccepted()) return;

    this.submitting.set(true);

    // Simuler un submit (remplace par ton appel API .NET 9)
    setTimeout(() => {
      const payload = this.form.value;
      console.log('Payload:', payload);
      this.submitting.set(false);
      // TODO: navigation, toast, reset, etc.
    }, 800);
  }
}
