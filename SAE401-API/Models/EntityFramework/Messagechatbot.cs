using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_messagechatbot_msg")]
public partial class Messagechatbot
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("msg_idmessage")]
    public int Idmessage { get; set; }

    [Column("msg_idclient")]
    public int Idclient { get; set; }

    [Column("msg_contenumessage")]
    [StringLength(512)]
    public string Contenumessage { get; set; } = null!;

    [Column("msg_reponsemessage")]
    [StringLength(1024)]
    public string Reponsemessage { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.MessagesNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;
}
