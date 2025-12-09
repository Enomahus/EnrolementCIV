using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    [Table("Coordinates")]
    public class CoordinateDao : EntityBaseDao<Guid>
    {
        [Phone]
        [StringLength(30)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [StringLength(150)]
        public string EmailAddress { get; set; }
        public string ResidenceAddress { get; set; }
        public string Locality { get; set; }
        public string Region { get; set; }

        public long? CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public CountryDao HostCountry { get; set; }
    }
}
