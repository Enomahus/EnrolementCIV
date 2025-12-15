using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Exceptions;
using Application.Exceptions.Auth;
using Application.Features.Users.Common;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.Errors;
using FluentValidation;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pcea.Core.Net.Authorization.Application.Attributes;
using Pcea.Core.Net.Authorization.Application.Interfaces.Services;
using Tools.Configuration;
using Tools.Logging;

namespace Application.Features.Users.UpdateUser
{
    [WithPermission(nameof(AppPermission.UpdateUser))]
    public class UpdateUserCommand : UserModel, IRequest<Result<Guid>>
    {
        public Guid UserId { get; set; }
    }

    public class UpdateUserCommandValidator : UserCommandValidatorBase<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(ReadOnlyDbContext context)
            : base(context)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ValidationErrorCode.Required.ToString());
        }

        protected override Task<bool> BeUniqueEmailAsync(
            UpdateUserCommand command,
            string email,
            CancellationToken cancellationToken
        )
        {
            return _context.Users.AllAsync(
                u => command.UserId == u.Id || u.UserName != email && u.Email != email,
                cancellationToken
            );
        }
    }

    public class UpdateUserCommandHandler(
        UserManager<UserDao> userManager,
        WritableDbContext context,
        TimeProvider timeProvider
    )
        : UserCommandHandlerBase(context, userManager, timeProvider),
            IRequestHandler<UpdateUserCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(
            UpdateUserCommand command,
            CancellationToken cancellationToken
        )
        {
            using var activity = ActivitySourceLog
                .CQRS.Start()
                .AddParameter(command, c => c.UserId);

            var userDao =
                await _context
                    .Users.Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(UserDao), command.UserId);

            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteInTransactionAsync(
                async () =>
                {
                    await MapToDaoAsync(command, userDao);

                    await _context.SaveChangesAsync(cancellationToken);
                },
                () => Task.FromResult(true)
            );

            return Result<Guid>.From(userDao.Id);
        }
    }
}
