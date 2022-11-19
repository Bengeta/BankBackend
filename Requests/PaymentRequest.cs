namespace VTBlockBackend.Requests;

public class PaymentRequest
{
    public double Amount { get; set; }
    public int walletFrom { get; set; }
    public int walletTo { get; set; }
}