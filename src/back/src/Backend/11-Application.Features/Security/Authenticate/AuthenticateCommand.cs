using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions.Auth;
using Application.Features.Security.Common;
using Application.Models;
using Application.Models.Errors;
using FluentValidation;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Tools.Logging;
using Tools.Serialization;

namespace Application.Features.Security.Authenticate
{
    public class AuthenticateCommand : IRequest<Result<TokenResponse>>
    {
        public string? UserName { get; set; }

        [SensitiveData]
        public string? Password { get; set; }
    }

    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage(ValidationErrorCode.Required.ToString());
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage(ValidationErrorCode.Required.ToString());
        }
    }

    public class AuthenticateCommandHandler(
        UserManager<UserDao> userManager,
        ITokenHelper tokenHelper,
        TimeProvider timeProvider
    ) : IRequestHandler<AuthenticateCommand, Result<TokenResponse>>
    {
        public async Task<Result<TokenResponse>> Handle(
            AuthenticateCommand request,
            CancellationToken cancellationToken
        )
        {
            using var activity = ActivitySourceLog.CQRS.Start();

            UserDao user = await tokenHelper.GetUserForAuthenticationAsync(request.UserName!);

            var passwordValid = await userManager.CheckPasswordAsync(user, request.Password!);
            var userIsActive =
                user.DisabledDate == null || user.DisabledDate > timeProvider.GetUtcNow();

            var validUser = passwordValid && userIsActive;
            if (!validUser)
            {
                throw new UserAuthenticationException(request.UserName!);
            }

            TokenResponse token = await tokenHelper.GenerateTokenAsync(user, cancellationToken);
            return Result<TokenResponse>.From(token);
        }
    }
}
