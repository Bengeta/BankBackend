using Microsoft.EntityFrameworkCore;
using VTBlockBackend.Data;
using VTBlockBackend.Enums;
using VTBlockBackend.Interfaces;
using VTBlockBackend.Models;
using VTBlockBackend.Models.DBTables;
using VTBlockBackend.Requests;
using VTBlockBackend.Responses;

namespace VTBlockBackend.Service;

public class WalletService : IWalletService
{
    private readonly IConfiguration _configuration;

    public WalletService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ResponseModel<WalletResponse>> GetWalletAsync(string token, int walletId)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var wallet = await context.Wallet.Where(x => x.id == walletId && x.User.token == token)
                .Select(x => new WalletResponse() {Id = x.id, Balance = x.Balance}).FirstOrDefaultAsync();
            return new ResponseModel<WalletResponse>() {ResultCode = ResultCode.Success, Data = wallet};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<WalletResponse>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<List<WalletResponse>>> GetWalletsAsync(string token)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var wallets = await context.Wallet.Where(x => x.User.token == token)
                .Select(x => new WalletResponse() {Id = x.id, Balance = x.Balance}).ToListAsync();
            return new ResponseModel<List<WalletResponse>> {ResultCode = ResultCode.Success, Data = wallets};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<List<WalletResponse>>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<List<bool>>> PayAsync(string token, PaymentRequest payments)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var walletFrom =
                await context.Wallet.FirstOrDefaultAsync(x => x.User.token == token && x.id == payments.walletFrom);
            if (walletFrom == null)
                return new ResponseModel<List<bool>>() {ResultCode = ResultCode.Failed};
            var walletTo = await context.Wallet.FirstOrDefaultAsync(x => x.id == payments.walletTo);
            if (walletTo == null)
                return new ResponseModel<List<bool>>() {ResultCode = ResultCode.Failed};
            walletFrom.Balance -= payments.Amount;
            walletTo.Balance += payments.Amount;
            await context.SaveChangesAsync();
            var transaction = new TransactionHistoryModel()
            {
                Amount = payments.Amount,
                Date = DateTime.Now,
                WalletFrom = payments.walletFrom,
                WalletTo = payments.walletTo
            };
            await context.Transaction.AddAsync(transaction);
            await context.SaveChangesAsync();
            return new ResponseModel<List<bool>>() {ResultCode = ResultCode.Success};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<List<bool>>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<bool>> CreateWalletAsync(string token, WalletRequest wallet)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var walletDb = new WalletModel()
            {
                Balance = wallet.Balance,
                User = await context.Users.FirstOrDefaultAsync(x => x.token == token)
            };
            await context.Wallet.AddAsync(walletDb);
            await context.SaveChangesAsync();
            return new ResponseModel<bool>() {ResultCode = ResultCode.Success, Data = true};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<bool>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<bool>> UpdateWalletAsync(string token, int id, WalletRequest wallet)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var walletDb =
                await context.Wallet.FirstOrDefaultAsync(x =>
                    x.User != null && x.id == id && x.User.token == token);
            if (walletDb == null)
                return new ResponseModel<bool>() {ResultCode = ResultCode.Failed};
            walletDb.Balance = wallet.Balance;
            await context.SaveChangesAsync();
            return new ResponseModel<bool>() {ResultCode = ResultCode.Success, Data = true};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<bool>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<bool>> DeleteWalletAsync(string token, int walletId)
    {
        try
        {
            await using var context = new ApplicationContext(_configuration);
            var walletDb = await context.Wallet.FirstOrDefaultAsync(x => x.id == walletId && x.User.token == token);
            if (walletDb == null)
                return new ResponseModel<bool>() {ResultCode = ResultCode.Failed};
            context.Wallet.Remove(walletDb);
            await context.SaveChangesAsync();
            return new ResponseModel<bool>() {ResultCode = ResultCode.Success, Data = true};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<bool>() {ResultCode = ResultCode.Failed};
        }
    }
}