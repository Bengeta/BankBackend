using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VTBlockBackend.Data;
using VTBlockBackend.Enums;
using VTBlockBackend.Models;
using VTBlockBackend.Models.DBTables;
using VTBlockBackend.Utils;

namespace VTBlockBackend.Data;

public class ApplicationContextSeeder
{
    private readonly ApplicationContext _applicationContext;

    public ApplicationContextSeeder(ApplicationContext _applicationContext)
    {
        this._applicationContext = _applicationContext;
    }

    public void Seed(string SecretKey, string Issuer, string Audience)
    {
        _applicationContext.Users.RemoveRange(_applicationContext.Users);
        _applicationContext.ImageStorage.RemoveRange(_applicationContext.ImageStorage);
        _applicationContext.SaveChanges();

        var users = new List<UserModel>();
        if (!_applicationContext.Users.Any())
        {
            var pas = Hash.GenerateHash("asdf");
            users.Add(new UserModel()
            {
                username = "Ilya",
                token = "qwe",
                password = pas.Key,
                salt = pas.Value,
                email = "ilya@"
            });
            users.Add(new UserModel()
            {
                username = "gsanov",
                token = "asdf",
                password = pas.Key,
                salt = pas.Value,
                email = "gsanov@"
            });
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, users[^1].username));
            claims.Add(new Claim(ClaimTypes.Email, users[^1].email));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            users[^1].token = new JwtSecurityTokenHandler().WriteToken(token);
            _applicationContext.Users.AddRange(users);
        }

        var photos = new List<ImageStorage>()
        {
            new ImageStorage()
            {
                imageUrl = "boxes/aecc95ec2064745bf81e09943529a0bb.jpg",
            },
            new ImageStorage()
            {
                imageUrl = "boxes/ArcoLinux_2022-02-16_14-51-59.png",
            },
            new ImageStorage()
            {
                imageUrl = "boxes/колкие.jpg",
            },
        };

    }
}