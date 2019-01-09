using Component.Utility;
using DAL.DomainModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModel.DomainModels
{
    public class CancellationReason : BaseModel
    {
        public int Id { get; set; }
        public CultureType Culture { get; set; }
        public virtual ICollection<CancellationReasonML> CancellationReasonMLsList { get; set; }
    }


    public class CancellationReasonML 
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public CultureType Culture { get; set; }

        [ForeignKey("CancellationReason")]
        public int CancellationReason_Id { get; set; }
        public CancellationReason CancellationReason { get; set; }
    }
}
