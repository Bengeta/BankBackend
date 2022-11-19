using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VTBlockBackend.Enums;
using VTBlockBackend.Interfaces;
using VTBlockBackend.Models;
using VTBlockBackend.Data;
using VTBlockBackend.Models;
using VTBlockBackend.Responses;
using VTBlockBackend.Utils;
using VTBlockBackend.Models.DBTables;

namespace VTBlockBackend.Service;

public class UserService:IUserService
{
    private ApplicationContext _context;
    private JwtSettings _options;

    public UserService(ApplicationContext context,IOptions<JwtSettings> options)
    {
        _context = context;
        _options = options.Value;
    }
    public async Task<ResponseModel<string>> Login(string email, string password)
    {
        try
        {
            var user = await _context.Users.Where(x => x.email == email).FirstOrDefaultAsync();
            if (user == null)
                return new ResponseModel<string>() {ResultCode = ResultCode.UserNotFound};

            var hashedPassword = Hash.GenerateHashFromSalt(password, user.salt);
            if (hashedPassword != user.password)
                return new ResponseModel<string>() {ResultCode = ResultCode.PasswordIncorrect};

            return new ResponseModel<string>() {ResultCode = ResultCode.Success, Data = user.token};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<string>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<string>> SignUp(string username, string password, string email)
    {
        try
        {
            var user = await _context.Users.Where(x => x.email == email).AnyAsync();
            if (user)
                return new ResponseModel<string>() {ResultCode = ResultCode.UserAlreadyExists};

            var hashAndSalt = Hash.GenerateHash(password);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim(ClaimTypes.Email, email));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            var newUser = new UserModel()
            {
                username = username,
                password = hashAndSalt.Key,
                salt = hashAndSalt.Value,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                email = email
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return new ResponseModel<string>() {ResultCode = ResultCode.Success, Data = newUser.token};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<string>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<UserModelResponse>> GetUser(string token)
    {
        try
        {
            var user = await _context.Users.Where(x => x.token == token).FirstOrDefaultAsync();
            if (user == null)
                return null;
            var ans = new UserModelResponse()
            {
                email = user.email,
                Username = user.username,
                Id = user.id,
            };

            return new ResponseModel<UserModelResponse>() {ResultCode = ResultCode.Success, Data = ans};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<UserModelResponse>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<string>> EditePassword(string token, string oldPassword, string newPassword)
    {
        try
        {
            var user = await _context.Users.Where(x => x.token == token).FirstOrDefaultAsync();
            if (user == null)
                return new ResponseModel<string>() {ResultCode = ResultCode.UserNotFound};

            var password = Hash.GenerateHashFromSalt(oldPassword, user.salt);
            if (user.password != password)
                return new ResponseModel<string>() {ResultCode = ResultCode.PasswordIncorrect};

            user.password = Hash.GenerateHashFromSalt(newPassword, user.salt);
            await _context.SaveChangesAsync();
            return new ResponseModel<string>() {ResultCode = ResultCode.Success};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<string>() {ResultCode = ResultCode.Failed};
        }
    }

    public async Task<ResponseModel<string>> EditeUsername(string token, string newUsername)
    {
        try
        {
            var user = await _context.Users.Where(x => x.token == token).FirstOrDefaultAsync();
            if (user == null)
                return new ResponseModel<string>() {ResultCode = ResultCode.UserNotFound};

            user.username = newUsername;
            await _context.SaveChangesAsync();
            return new ResponseModel<string>() {ResultCode = ResultCode.Success};
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new ResponseModel<string>() {ResultCode = ResultCode.Failed};
        }
    }

    public Task<ResponseModel<string>> AddDevice(string token, string deviceToken)
    {
        throw new NotImplementedException();
    }
}