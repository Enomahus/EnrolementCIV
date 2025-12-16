using Infrastructure.Persistence.Entities;

namespace Application.Features.Countries.GetAllCountries
{
    public class GetCountriesResponse
    {
        public long Id { get; set; }
        public string CodeIso { get; set; } = string.Empty;
        public string DialCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static GetCountriesResponse From(CountryDao dao)
        {
            return new GetCountriesResponse
            {
                Id = dao.Id,
                CodeIso = dao.CodeIso,
                DialCode = dao.DialCode,
                Name = dao.Name,
            };
        }
    }
}
