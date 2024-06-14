using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DationBus.Domain.Models;

[Table("QueueStatus", Schema = "queue")]
[Index("QueueStatusName", Name = "UK_QueueStatus", IsUnique = true)]
public partial class QueueStatus
{
    [Key]
    public int QueueStatusId { get; set; }

    [StringLength(10)]
    public string QueueStatusName { get; set; } = null!;

    [InverseProperty("StatusNameNavigation")]
    public virtual ICollection<QueueLog> QueueLogs { get; set; } = new List<QueueLog>();
}
