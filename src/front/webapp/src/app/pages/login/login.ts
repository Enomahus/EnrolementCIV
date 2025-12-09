import { Component, computed, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
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
  public userName = signal('');
  public password = signal('');
  isLoggingIn = signal(false);
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
    if (this.isFormValid()) {
      console.log('Connexion avec :', {
        username: this.userName(),
        password: this.password(),
      });
      if (this.userName() === 'bolan' && this.password() === 'Secret01') {
        this.router.navigateByUrl('/register');
      }
    } else {
      console.warn('Formulaire invalide');
    }
  }

  togglePasswordVisible(): void {
    this.passwordVisible.set(!this.passwordVisible());
  }
}
