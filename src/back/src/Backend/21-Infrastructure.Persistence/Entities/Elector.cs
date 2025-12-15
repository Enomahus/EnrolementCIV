using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    //Electeurs
    public class Elector : EntityBaseDao<Guid>, ITimestampedEntity
    {
        public string VoterNumber { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public bool IsActif { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CitizenId { get; set; }

        [ForeignKey(nameof(CitizenId))]
        public CitizenDao Citizen { get; set; }

        public long ConstituencyId { get; set; }

        [ForeignKey(nameof(ConstituencyId))]
        public ConstituencyDao Constituencies { get; set; }
    }
}
