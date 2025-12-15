using Application.Common.Enums;
using Application.Features.Users.Common;
using Application.Models;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pcea.Core.Net.Authorization.Application.Attributes;
using Tools.Logging;

namespace Application.Features.Users.CreateUser
{
    [WithPermission(nameof(AppPermission.CreateUser))]
    public class CreateUserCommand : UserModel, IRequest<Result<Guid>> { }

    public class CreateUserCommandValidator(ReadOnlyDbContext context)
        : UserCommandValidatorBase<CreateUserCommand>(context) { }

    public class CreateUserCommandHandler(
        WritableDbContext context,
        UserManager<UserDao> userManager,
        TimeProvider timeProvider
    )
        : UserCommandHandlerBase(context, userManager, timeProvider),
            IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private const StringComparison ignoreCase = StringComparison.CurrentCultureIgnoreCase;

        public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken ct)
        {
            using var activity = ActivitySourceLog.CQRS.Start();

            var userDao = new UserDao();

            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteInTransactionAsync(
                async () =>
                {
                    await MapToDaoAsync(command, userDao);

                    await _userManager.CreateAsync(userDao, command.Password!);
                },
                () => Task.FromResult(true)
            );

            activity.AddParameter(userDao, u => u.Id);

            return Result<Guid>.From(userDao.Id);
        }
    }
}
