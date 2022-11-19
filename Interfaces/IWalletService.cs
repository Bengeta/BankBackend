using VTBlockBackend.Models;
using VTBlockBackend.Models.DBTables;
using VTBlockBackend.Requests;
using VTBlockBackend.Responses;

namespace VTBlockBackend.Interfaces;

public interface IWalletService
{
    public Task<ResponseModel<WalletResponse>> GetWalletAsync(string token, int walletId);
    public Task<ResponseModel<bool>> CreateWalletAsync(string token, WalletRequest wallet);
    public Task<ResponseModel<bool>> UpdateWalletAsync(string token, int id,WalletRequest wallet);
    public Task<ResponseModel<bool>> DeleteWalletAsync(string token, int walletId);
    public Task<ResponseModel<List<WalletResponse>>> GetWalletsAsync(string token);
    public Task<ResponseModel<List<bool>>> PayAsync(string token, PaymentRequest payments);
}