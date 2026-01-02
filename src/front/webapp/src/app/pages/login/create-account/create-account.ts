import { CommonModule } from '@angular/common';
import { Component, computed, effect, inject, OnInit, signal } from '@angular/core';
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
import { UserService } from '@app/services/api/user-api.service';
import { PersonTitle, RoleModel } from '@app/services/nswag/api-nswag-client';
import { passwordMatchValidator } from '@app/shared/helpers/for-helper';
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
  ],
  templateUrl: './create-account.html',
  styleUrl: './create-account.scss',
})
export class CreateAccount implements OnInit {
  private readonly userService = inject(UserService);

  // --- Signals d’UI ---
  showPassword = signal(false);
  showConfirm = signal(false);
  termsAccepted = signal(false);
  submitting = signal(false);
  roles = signal<RoleModel[]>([]);

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
        isActive: new FormControl<boolean>(true, {
          validators: [Validators.required],
        }),
        roles: new FormControl<string[]>([]),
        phoneNumber: new FormControl<string | undefined>(undefined, {
          validators: [Validators.required],
        }),
        matricule: new FormControl<string | undefined>(undefined),
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

  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles(): void {
    this.userService.getAllRoles().subscribe((data) => this.roles.set(data));

    this.form.get('roles')?.valueChanges.subscribe((roleValue) => {
      const matriculeCtrl = this.form.get('matricule');
      if (
        roleValue === 'CommissionChairmanRole' ||
        roleValue === 'SupervisorRole' ||
        roleValue === 'DataEntryOperatorRole'
      ) {
        matriculeCtrl?.addValidators(Validators.required);
      } else {
        matriculeCtrl?.removeValidators(Validators.required);
      }
      matriculeCtrl?.updateValueAndValidity();
    });
  }

  onPhoneChange(value: string): void {
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
