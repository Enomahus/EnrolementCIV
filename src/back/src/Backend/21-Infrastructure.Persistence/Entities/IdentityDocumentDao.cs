using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Enums;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    [Table("IdentityDocuments")]
    public class IdentityDocumentDao : EntityBaseDao<Guid>
    {
        [Required]
        public DocumentType DocumentType { get; set; }

        [StringLength(60)]
        public string DocumentNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
    }
}
