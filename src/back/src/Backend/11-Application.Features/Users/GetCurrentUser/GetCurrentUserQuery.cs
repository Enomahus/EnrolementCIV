using Application.Common.Enums;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Models;
using FluentValidation;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pcea.Core.Net.Authorization.Application.Attributes;
using Pcea.Core.Net.Authorization.Application.Interfaces.Services;
using Tools.Logging;

namespace Application.Features.Users.GetCurrentUser
{
    [WithPermission(nameof(AppPermission.GetCurrentUser))]
    public class GetCurrentUserQuery : IRequest<Result<GetCurrentUserResponse>> { }

    public class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
    {
        public GetCurrentUserQueryValidator() { }
    }

    public class GetCurrentUserQueryHandler(
        ICurrentUserService currentUserService,
        ICurrentUserPermissionsProvider currentUserPermissionsProvider,
        ReadOnlyDbContext context,
        TimeProvider timeProvider
    ) : IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse>>
    {
        public async Task<Result<GetCurrentUserResponse>> Handle(
            GetCurrentUserQuery request,
            CancellationToken cancellationToken
        )
        {
            using var activity = ActivitySourceLog.CQRS.Start();

            var userId = currentUserService.UserId;
            var currentUser =
                await context
                    .Users.Include(u => u.UserRoles)
                    .ThenInclude(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                ?? throw new NotFoundException(nameof(UserDao), userId);

            var permissions = await currentUserPermissionsProvider.GetCurrentUserPermissionsAsync(
                cancellationToken
            );

            return Result<GetCurrentUserResponse>.From(
                GetCurrentUserResponse.FromDao(
                    currentUser,
                    [.. permissions.Select(Enum.Parse<AppPermission>)],
                    timeProvider.GetUtcNow()
                )
            );
        }
    }
}
