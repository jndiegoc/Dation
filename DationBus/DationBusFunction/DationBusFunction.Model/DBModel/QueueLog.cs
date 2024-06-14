using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DationBusFunction.Model.DBModel
{
    [Table("QueueLog", Schema = "queue")]
    public partial class QueueLog
    {
        [Key]
        public int QueueLogId { get; set; }

        public string QueueMessage { get; set; } = null!;

        [StringLength(10)]
        public string StatusName { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("StatusName")]
        [InverseProperty("QueueLogs")]
        public virtual QueueStatus StatusNameNavigation { get; set; } = null!;
    }
}
