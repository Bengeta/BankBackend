using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTBlockBackend.Interfaces;
using VTBlockBackend.Models;
using VTBlockBackend.Requests;
using VTBlockBackend.Responses;

namespace VTBlockBackend.Controllers;

[ApiController]
[Authorize]
public class WalletController : BaseController
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }
    
    [HttpGet("wallets/{id:int}")]
    public async Task<ResponseModel<WalletResponse>> GetWallet(int id)
    {
        var token = Token();
        return await _walletService.GetWalletAsync(Token(),id);
    }
    
    [HttpGet("wallets")]
    public async Task<ResponseModel<List<WalletResponse>>> GetWallets()
    {
        var token = Token();
        return await _walletService.GetWalletsAsync(Token());
    }
    
    [HttpPost("wallets")]
    public async Task<ResponseModel<bool>> CreateWallet([FromBody] WalletRequest request)
    {
        var token = Token();
        return await _walletService.CreateWalletAsync(Token(),request);
    }
    
    [HttpPut("wallets/{id:int}")]
    public async Task<ResponseModel<bool>> UpdateWallet(int id, [FromBody] WalletRequest request)
    {
        var token = Token();
        return await _walletService.UpdateWalletAsync(Token(),id,request);
    }
    
    [HttpDelete("wallets/{id:int}")]
    public async Task<ResponseModel<bool>> DeleteWallet(int id)
    {
        var token = Token();
        return await _walletService.DeleteWalletAsync(Token(),id);
    }
}