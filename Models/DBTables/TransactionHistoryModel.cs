using System.ComponentModel.DataAnnotations.Schema;

namespace VTBlockBackend.Models.DBTables;

[Table("TransactionHistory")]
public class TransactionHistoryModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public double Amount { get; set; }
    public int WalletFrom { get; set; }
    public int WalletTo { get; set; }
    public DateTime Date { get; set; }
}