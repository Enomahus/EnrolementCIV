import { Component, computed, inject, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { SecurityApiService } from '@app/services/api/security-api.service';
import { Loader } from '@app/shared/loader/loader';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { LoginTemplate } from '../login/login-template/login-template';

const patternEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/i;

@Component({
  selector: 'app-forgot-password',
  imports: [RouterLink, ReactiveFormsModule, TranslateModule, LoginTemplate, Loader],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.scss',
})
export class ForgotPassword {
  private readonly translateService = inject(TranslateService);
  private readonly securityService = inject(SecurityApiService);

  public email = signal('');
  emailTouched = signal(false);
  loading = signal(false);
  mailAlreadySent = signal(false);

  errorEmail = computed(() => {
    const emailValue = this.email();

    if (!this.emailTouched()) return null;
    if (!emailValue) return this.translateService.instant('formsError.required');

    return patternEmail.test(emailValue)
      ? null
      : this.translateService.instant('formsError.emailInvalid');
  });

  isFormValid = computed(() => !this.errorEmail());

  onSubmit(): void {
    if (!this.isFormValid()) return;

    this.loading.set(true);

    this.securityService
      .forgotPassword(this.email(), {
        successMessage: this.translateService.instant('forgotPassword.success'),
      })
      .subscribe({
        next: () => {
          this.mailAlreadySent.set(true);
          this.loading.set(false);
        },
        error: () => {
          this.loading.set(false);
        },
      });
  }
}
