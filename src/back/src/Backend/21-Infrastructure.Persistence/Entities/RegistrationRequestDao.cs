using Application.Common.Enums;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    //Demande d'inscription
    public class RegistrationRequestDao : EntityBaseDao<Guid>
    {
        public DateTimeOffset SoumissionDate { get; set; }
        public RegistrationStatus Status { get; set; }
        public string ReasonForRejection { get; set; }
        public virtual ICollection<SupportingDocumentsDao> SupportingDocuments { get; set; } = [];
    }
}
