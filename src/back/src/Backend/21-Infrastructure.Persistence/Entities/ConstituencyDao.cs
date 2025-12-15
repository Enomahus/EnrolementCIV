using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Enums;
using Infrastructure.Persistence.Common;

namespace Infrastructure.Persistence.Entities
{
    //Circonscription
    public class ConstituencyDao : EntityBaseDao<long>
    {
        public string Name { get; set; }
        public string OfficialCode { get; set; }
        public ConstituencyType Type { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public ConstituencyDao ConstituencyParent { get; set; }

        public virtual ICollection<Elector> Voters { get; set; } = [];
    }
}
