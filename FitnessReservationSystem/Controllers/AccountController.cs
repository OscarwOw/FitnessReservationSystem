using FitnessReservationSystem.Configuration;
using FitnessReservationSystem.Data;
using FitnessReservationSystem.Dto.UserDtos;
using FitnessReservationSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly JwtConfig _jwtConfig;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ILogger<AccountController> _logger;
        private readonly DatabaseContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration, TokenValidationParameters tokenValidationParameters, ILogger<AccountController> logger, DatabaseContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _logger = logger;
            _context = context;
            //_jwtConfig = jwtConfig; 
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto requestDto)
        {
            //Validate incoming request
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            //We need to check if email already exists.
            var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user_exist != null) 
            {
                return BadRequest();
            }
            //Create user
            var new_user = new ApplicationUser()
            {
                Email = requestDto.Email,
                UserName = requestDto.Username
            };
            var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);
            if (!is_created.Succeeded)
            {
                return BadRequest();
            }
            //Generate Token 
            var token = await GenerateJwtToken(new_user);
            return Ok(token);

        }
        
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }
            //check if user exists
            var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if(existing_user == null)
            {
                return BadRequest();
            }
            var isCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);
            if (!isCorrect) { return BadRequest(); }
            var jwtToken = await GenerateJwtToken(existing_user);
            return Ok(jwtToken);

        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid) {
                return BadRequest(new AuthResult
                {
                    Errors = new List<string>
                    {
                        "Invalid Parameters"
                    },
                    Result = false
                }) ;
            }
            var result = await VerifyAndGenerateToken(tokenRequest);

            if(result == null)
            {
                return BadRequest(new AuthResult
                {
                    Errors = new List<string>
                    {
                        "Invalid Token"
                    },
                    Result = false
                });
            }
            if(result.Token == null)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                _tokenValidationParameters.ValidateLifetime = false;
                var tokenInVefification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase);
                    if(result == false)
                    {
                        return null;
                    }
                }
                var utcExpiryDate = long.Parse(tokenInVefification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
                if(expiryDate == DateTime.Now)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>() {
                            "Expired token"
                        }
                    };
                }
                var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
                if(storedToken == null) {
                    return new AuthResult()
                    {
                        Result =  false,
                        Errors = new List<string>() {
                            "Invalid tokens"
                        }
                    };
                }
                if(storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>() {
                            "Invalid tokens"
                        }
                    };
                }
                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>() {
                            "Invalid tokens"
                        }
                    };
                }
                var jti = tokenInVefification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>() {
                            "Invalid tokens"
                        }
                    };
                }
                if(storedToken.ExpiryDate < DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>() {
                            "Expired tokens"
                        }
                    };
                }
                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch(Exception ex) { }
            {
                
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>() {
                            "Server error"
                        }
                };

            }
        }
        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTimeVal;
        }

        private async Task<AuthResult> GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                Token = RandomStringGeneration(23),//Generate a refresh token
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id //change to id only
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            var result = new AuthResult()
            {
                Result = true,
                RefreshToken = refreshToken.Token,
                Token = jwtToken
            };

            return result;
        }

        private string RandomStringGeneration(int lenght)
        {
            var random = new Random();
            var chars = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789qwertyuiopasdfghjklzxcvbnm_";
            return new string(Enumerable.Repeat(chars, lenght).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
