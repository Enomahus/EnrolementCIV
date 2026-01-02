import { AbstractControl } from '@angular/forms';
import { PhoneNumberUtil } from 'google-libphonenumber';

export function phoneNumberValidator() {
  return (control: AbstractControl): Record<string, Record<string, boolean>> | null => {
    const value = control.value;
    if (!value) {
      return null;
    }

    const number = PhoneNumberUtil.getInstance().parse(value);
    if (number && PhoneNumberUtil.getInstance().isValidNumber(number)) {
      return null;
    }

    return { pattern: { valid: false } };
  };
}
