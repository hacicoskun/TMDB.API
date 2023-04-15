using HC.Api.Identity.Identity;
using HC.Presentation.API.Authentication;
using HC.Shared.Application.Interfaces;
using HC.Shared.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HC.Presentation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApiIdentityUser> userManager;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IPostgreDbContext _db;

    public AuthenticateController(UserManager<ApiIdentityUser> userManager, IConfiguration configuration, IHostEnvironment hostEnvironment, PostgreDbContext db)
    {
        this.userManager = userManager;
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        #region CreateNewUserForIdentity
        //string password = "Haci123!?";
         
        //ApiIdentityUser apiIdentityUser = new ApiIdentityUser
        //{
        //    AccessFailedCount = 0,
        //    ConcurrencyStamp = "1681455546000",
        //    Email = "hacicoskun07@gmail.com",
        //    EmailConfirmed = true,
        //    Id = Guid.NewGuid().ToString(),
        //    LockoutEnabled = false,
        //    NormalizedEmail = "hacicoskun07@gmail.com",
        //    NormalizedUserName = "hacicoskun07@gmail.com",
        //    PasswordHash = password,
        //    PhoneNumber = "5423456079",
        //    PhoneNumberConfirmed = true,
        //    SecurityStamp = "1681455546000",
        //    TwoFactorEnabled = false,
        //    UserName = "hacicoskun07@gmail.com"

        //};
        //var result = await userManager.CreateAsync(apiIdentityUser, password);

        #endregion

        var user = await userManager.FindByNameAsync(model.Username);

        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
           

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));


            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
           

            JwtSecurityToken? token;
            if (_hostEnvironment.IsDevelopment())
            {
                token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            }
            else
            {
                token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            }



            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }
}
 