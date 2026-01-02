using Application.Common.Enums;
using Application.Models;
using FluentValidation;
using Infrastructure.Persistence.SQLServer.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pcea.Core.Net.Authorization.Application.Attributes;
using Tools.Logging;

namespace Application.Features.Users.GetAllRoles
{
    //[WithPermission(nameof(AppPermission.GetAllRoles))]
    public class GetAllRolesQuery : IRequest<Result<List<RoleModel>>> { }

    public class GetAllRolesQueryValidator : AbstractValidator<GetAllRolesQuery> { }

    public class GetAllRolesQueryHandler(ReadOnlyDbContext context)
        : IRequestHandler<GetAllRolesQuery, Result<List<RoleModel>>>
    {
        public async Task<Result<List<RoleModel>>> Handle(
            GetAllRolesQuery request,
            CancellationToken cancellationToken
        )
        {
            using var activity = ActivitySourceLog.CQRS.Start();

            var roles = await context
                .Roles.Select(r => new RoleModel() { Name = r.Name, Id = r.Id })
                .ToListAsync(cancellationToken);

            return Result<List<RoleModel>>.From(roles);
        }
    }
}
