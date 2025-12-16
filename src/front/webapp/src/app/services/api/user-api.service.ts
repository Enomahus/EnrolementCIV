import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CreateUserCommand,
  GetCurrentUserResponse,
  ResultOfGuid,
  RoleModel,
  UpdateUserCommand,
  UserModel,
} from '../nswag/api-nswag-client';
import { ApiBaseService } from './api-base.service';
import { ApiToastOptions } from './models/api-toast-options';

@Injectable({
  providedIn: 'root',
})
export class UserService extends ApiBaseService {
  getCurrentUser(options: ApiToastOptions = {}): Observable<GetCurrentUserResponse> {
    return this.apiClient.getCurrentUser().pipe(
      this.handleDataResult({
        ...options,
        errorMessage:
          options.errorMessage ?? this.translateService.instant('global.errorGetCurrentUser'),
      }),
    );
  }

  createUser(command: CreateUserCommand, options: ApiToastOptions = {}): Observable<ResultOfGuid> {
    return this.apiClient.createUser(command).pipe(this.handleResult(options));
  }

  updateUser(
    id: string,
    command: UpdateUserCommand,
    options: ApiToastOptions = {},
  ): Observable<ResultOfGuid> {
    return this.apiClient.updateUser(id, command).pipe(this.handleResult(options));
  }

  getUser(id: string, options: ApiToastOptions = {}): Observable<UserModel> {
    return this.apiClient.getUser(id).pipe(this.handleDataResult(options));
  }

  getAllRoles(options: ApiToastOptions = {}): Observable<RoleModel[]> {
    return this.apiClient.getAllRoles().pipe(this.handleDataResult(options));
  }
}
