using System.ComponentModel.DataAnnotations;
using Application.Common.Enums;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    //Citoyens
    public class CitizenDao : EntityBaseDao<Guid>, ITimestampedEntity
    {
        [StringLength(35)]
        public string Nni { get; set; }

        [Required, StringLength(80)]
        public string FirstName { get; set; }

        [Required, StringLength(60)]
        public string LastName { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        [Required, StringLength(60)]
        public string MarriedName { get; set; }

        [Required]
        public DateOnly BirthDay { get; set; }

        [Required]
        public string BirthPlace { get; set; }
        public Gender Gender { get; set; }

        [Required]
        public string Nationality { get; set; } = "Ivoirienne";

        [StringLength(150)]
        public string? Occupation { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public CoordinateDao Coordinate { get; set; } = new();
        public IdentityDocumentDao IdentityDocument { get; set; } = new();
        public virtual ICollection<VoterDao> Voters { get; set; } = [];
        public virtual ICollection<RegistrationRequestDao> RegistrationRequests { get; set; } = [];

        // Navigation : Filiation
        public ICollection<FiliationDao> Filiations { get; set; } = [];
    }
}
