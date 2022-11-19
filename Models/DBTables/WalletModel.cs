using System.ComponentModel.DataAnnotations.Schema;

namespace VTBlockBackend.Models.DBTables;

[Table("Wallet")]
public class WalletModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    [ForeignKey("User")] public int UserId { get; set; }
    public UserModel? User { get; set; }
    public double Balance { get; set; }
}