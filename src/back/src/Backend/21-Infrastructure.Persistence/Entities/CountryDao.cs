using System.ComponentModel.DataAnnotations;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    public class CountryDao : EntityBaseDao<long>
    {
        [MaxLength(2)]
        public required string CodeIso { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public virtual ICollection<CoordinateDao> Coordinates { get; set; } = [];
    }
}
