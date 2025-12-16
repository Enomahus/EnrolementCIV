using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using FluentValidation;
using Infrastructure.Persistence.SQLServer.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tools.Logging;

namespace Application.Features.Countries.GetAllCountries
{
    public class GetCountriesQuery : IRequest<Result<List<GetCountriesResponse>>>
    {
        public GetCountriesQuery() { }
    }

    public class GetCountriesQueryValidator : AbstractValidator<GetCountriesQuery>
    {
        public GetCountriesQueryValidator() { }
    }

    public class GetCountriesQueryHandler(ReadOnlyDbContext context)
        : IRequestHandler<GetCountriesQuery, Result<List<GetCountriesResponse>>>
    {
        public async Task<Result<List<GetCountriesResponse>>> Handle(
            GetCountriesQuery request,
            CancellationToken cancellationToken
        )
        {
            using var activity = ActivitySourceLog.CQRS.Start();

            var countries = await context.Countries.ToListAsync(cancellationToken);

            return Result<List<GetCountriesResponse>>.From(
                countries.Select(GetCountriesResponse.From).ToList()
            );
        }
    }
}
