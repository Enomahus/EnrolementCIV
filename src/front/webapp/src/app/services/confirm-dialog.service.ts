import { inject, Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmDialog } from '@app/shared/confirm-dialog/confirm-dialog';

import { TranslateService } from '@ngx-translate/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConfirmDialogService {
  private readonly translateService = inject(TranslateService);
  private readonly dialog = inject(MatDialog);
  private dialogSubject = new Subject<boolean>();

  confirmationDialog(): Observable<boolean> {
    this.dialogSubject = new Subject<boolean>();
    this.openDialog();
    return this.dialogSubject.asObservable();
  }

  private openDialog(): void {
    const config = new MatDialogConfig();
    config.width = '450px';
    config.height = '200px';
    config.disableClose = true;
    config.autoFocus = true;
    config.data = {
      title: this.translateService.instant('global.quitWithoutSaving'),
      content: this.translateService.instant('global.returnText'),
      cancelText: this.translateService.instant('global.cancel'),
      confirmText: this.translateService.instant('global.confirm'),
    };

    const dialogRef = this.dialog.open(ConfirmDialog, config);

    dialogRef.afterClosed().subscribe((result: boolean) => {
      this.dialogSubject.next(result === true);
      this.dialogSubject.complete();
    });
  }
}
