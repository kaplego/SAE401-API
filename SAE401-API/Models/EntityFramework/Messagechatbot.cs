using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("messagechatbot")]
[Index("Idclient", Name = "clientchatbot_fk")]
[Index("Idmessage", Name = "messagechatbot_pk", IsUnique = true)]
public partial class Messagechatbot
{
    [Key]
    [Column("idmessage")]
    public int Idmessage { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("contenumessage")]
    [StringLength(512)]
    public string Contenumessage { get; set; } = null!;

    [Column("reponsemessage")]
    [StringLength(1024)]
    public string Reponsemessage { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Messagechatbots")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
