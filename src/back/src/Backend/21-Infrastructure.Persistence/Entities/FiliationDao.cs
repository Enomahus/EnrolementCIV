using System.ComponentModel.DataAnnotations;
using Application.Common.Enums;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    public class FiliationDao : EntityBaseDao<Guid>
    {
        // Sujet de la filiation (la personne dont on décrit la relation)
        public Guid CitoyenId { get; set; }
        public CitizenDao Citoyen { get; set; } = default!;

        // L’autre personne liée (père/mère/tuteur/conjoint/enfant)
        public Guid RelatifId { get; set; }
        public CitizenDao Relatif { get; set; } = default!;

        [Required]
        public TypeOfRelationship TypeLien { get; set; }

        [StringLength(120)]
        public string Proof { get; set; } //Ex: Extrait de naissance
    }
}
