import { AbstractControl, ValidatorFn } from '@angular/forms';

export function passwordMatchValidator(password: string, passwordConfirm: string): ValidatorFn {
  return (control: AbstractControl): Record<string, boolean> | null => {
    const passwordControl = control.get(password);
    const passwordConfirmControl = control.get(passwordConfirm);

    if (!passwordControl || !passwordConfirmControl) {
      return null;
    }

    const mismatch = passwordControl.value !== passwordConfirmControl.value;
    return mismatch ? { passwordMismatch: true } : null;
  };
}

export function positiveDecimalValidator(isStrict: boolean): ValidatorFn {
  return (control: AbstractControl) => {
    const value = control.value;

    const isStrictPositive = isStrict && value > 0;
    const isPositive = !isStrict && value >= 0;

    if (value === null || value === undefined || isStrictPositive || isPositive) {
      return null;
    }

    return { positiveDecimal: { valid: false } };
  };
}

export function positiveIntegerValidator(isStrict: boolean) {
  return (control: AbstractControl): Record<string, Record<string, boolean>> | null => {
    const value = control.value;

    const isStrictPositive = isStrict && value > 0;
    const isPositive = !isStrict && value >= 0;

    if (
      value === null ||
      value === undefined ||
      (Number.isInteger(value) && (isStrictPositive || isPositive))
    ) {
      return null;
    }

    return { positiveInteger: { valid: false } };
  };
}
