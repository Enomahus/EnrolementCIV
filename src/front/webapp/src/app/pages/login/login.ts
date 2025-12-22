import { Component, computed, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '@app/services/auth/auth.service';
import { Loader } from '@app/shared/loader/loader';
import { TranslateModule } from '@ngx-translate/core';
import { LoginTemplate } from './login-template/login-template';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, TranslateModule, LoginTemplate, Loader],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);
  public userName = signal('');
  public password = signal('');
  isLoggingIn = signal(false);
  loginFailed = signal(false);
  passwordVisible = signal(false);
  userNameTouched = signal(false);
  passwordTouched = signal(false);

  errorUserName = computed(() => {
    const value = this.userName();

    return this.userNameTouched() && !value ? 'Champ requis' : null;
  });

  errorPassword = computed(() => {
    const value = this.password();

    if (!this.passwordTouched()) return null;
    if (!value) return 'Champ requis';
    if (value.length < 8) return 'Minimum 8 caractères nécessaires.';

    return null;
  });

  isFormValid = computed(() => !this.errorUserName() && !this.errorPassword());

  onSubmit(): void {
    if (!this.isFormValid()) return;

    this.isLoggingIn.set(true);
    this.authService.login(this.userName(), this.password()).subscribe({
      next: () => {
        this.isLoggingIn.set(false);
        this.router.navigateByUrl('/home');
      },
      error: () => {
        this.isLoggingIn.set(false);
        this.loginFailed.set(true);
      },
    });
  }

  togglePasswordVisible(): void {
    this.passwordVisible.set(!this.passwordVisible());
  }
}
