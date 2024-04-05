using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Kvdemy.Web.Data;
using Kvdemy.Data.Models;
using Kvdemy.Core.Options;
using Kvdemy.Core.Resourses;
using Kvdemy.Web.Services;
using Kvdemy.Core.ViewModels;
using Krooti.Core.Enums;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Exceptions;
using Krooti.Infrastructure.Services.Auth;
using Kvdemy.Core.Dtos;
using Krooti.Core.ViewModels;



namespace Kvdemy.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {



        private readonly KvdemyDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtOptions _options;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public AuthService(
            IMapper mapper,
            KvdemyDbContext db,
            RoleManager<IdentityRole> roleManger,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
           IOptions<JwtOptions> options,
        IStringLocalizer<Messages> localizedMessages,
            SignInManager<User> signInManager,
            IFileService fileService
            )
        {

            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            _options = options.Value;

            _localizedMessages = localizedMessages;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManger = roleManger;
            _fileService = fileService;
        }

        //public async Task<dynamic> Login(LoginDto dto)
        //{
        //    var user = _db.Users.SingleOrDefault(x => x.PhoneNumber == dto.PhoneNumber);
        //    if (user == null)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);

        //    }
        //    else
        //    {
        //        Random random = new Random();
        //        var generatedOtp = random.Next(1000, 10000);
        //        user.OtpCode = generatedOtp.ToString();
        //        _db.Users.Update(user);
        //        _db.SaveChanges();
        //        //await _emailService.Send(user.Email, "Otp Code !", $"Otp is : {user.OtpCode}");
        //    }
        //    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.OtpSuccess], user.OtpCode);



        //}

        public async Task<dynamic> CheckOtpCode(OtpCodeDto dto)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);


            }
            if (user.OtpCode != dto.OtpCode)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.OtpWrong]);

            }

            var response = new LoginResponseViewModel();
            await AddUserToRole(user, UserType.Student);

            response.AcessToken = await GenerateAccessToken(user, UserType.Student);
            response.student = _mapper.Map<StudentViewModel>(user);


            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.LoginSuccess], response);


        }

        //public async Task<dynamic> LoginAdmin(LoginAdminDto dto)
        //{
        //    var user = await _userManager.FindByNameAsync(dto.UserName);
        //    if (user == null)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.InvalidCredentials]);

        //    }
        //    else
        //    {

        //        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        //        if (result.Succeeded)
        //        {
        //            var response = new LoginAdminResponseViewModel();
        //            await AddUserToRole(user, UserType.Admin);


        //            response.AcessToken = await GenerateAccessToken(user, UserType.Admin);
        //            response.AdminInfo = _mapper.Map<AdminViewModel>(user);




        //            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.LoginSuccess], response);
        //        }
        //        else
        //        {
        //            return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.InvalidCredentials]);
        //        }
        //    }

        //}

        public async Task<AccessTokenViewModel> GenerateAccessToken(User user, UserType userType)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>(){
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(Claims.Phone, user.PhoneNumber),
              new Claim(Claims.UserId,user.Id),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));


                if (userType == UserType.SuperAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "SuperAdmin"));
                }
                else if (userType == UserType.Admin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else if (userType == UserType.Teacher)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Student"));
                }


            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMonths(7);

            var accessToken = new JwtSecurityToken(_options.Issure,
               _options.Issure,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            var result = new AccessTokenViewModel()
            {
                AcessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                ExpireAt = expires
            };


            return result;
        }


        public async Task AddUserToRole(User user, UserType userType)
        {
            await InitRoles();
            if (userType == UserType.SuperAdmin)
            {
                await _userManager.AddToRoleAsync(user, "SuperAdmin");
            }
            else if (userType == UserType.Admin)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else if (userType == UserType.Teacher)
            {
                await _userManager.AddToRoleAsync(user, "Teacher");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Student");
            }


        }
        //public async Task<dynamic> LogOut()
        //{

        //    string jwtToken = _httpContextAccessor.HttpContext.Request.Headers.Authorization;

        //    // Remove the "Bearer " prefix to get just the token
        //    if (!string.IsNullOrEmpty(jwtToken) && jwtToken.StartsWith("Bearer "))
        //    {
        //        jwtToken = jwtToken.Substring("Bearer ".Length);
        //    }
        //    else
        //    {
        //        return new ApiResponseFailedViewModel("Invalid Authorization header format");
        //    }

        //    // Now 'accessToken' contains the token value
        //    // You can use it as needed in your controller logic



        //    // Configure token validation parameters

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,

        //        // Replace these values with your actual values
        //        ValidIssuer = _options.Issure,

        //        IssuerSigningKey = key,
        //    };

        //    // Create a token handler
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {
        //        // Validate and read the token
        //        var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken validatedToken);

        //        // Extract user data from claims
        //        string userId = claimsPrincipal.FindFirst(Claims.UserId)?.Value;
        //        string username = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        //        string phoneNumber = claimsPrincipal.FindFirst(Claims.Phone)?.Value;

        //        // Use the extracted user data as needed
        //        User user = new User();
        //        user.Id = userId;
        //        user.UserName = username;
        //        user.PhoneNumber = phoneNumber;

        //        var expires = DateTime.Now;
        //        await GenerateAccessToken(user, UserType.Client);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Token validation failed: {ex.Message}");
        //    }
        //    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataFailed], null);
        //}
        public async Task<dynamic> InitRoles()
        {
            if (!_db.Roles.Any())
            {
                var roles = new List<string>();
                roles.Add("SuperAdmin");
                roles.Add("Admin");
                roles.Add("Teacher");
                roles.Add("Student");

                foreach (var role in roles)
                {
                    await _roleManger.CreateAsync(new IdentityRole(role));
                }
            }
            return true;

        }

        public async Task<string> GetUserIdFromToken()
        {


            string jwtToken = _httpContextAccessor.HttpContext.Request.Headers.Authorization;

            // Remove the "Bearer " prefix to get just the token
            if (!string.IsNullOrEmpty(jwtToken) && jwtToken.StartsWith("Bearer "))
            {
                jwtToken = jwtToken.Substring("Bearer ".Length);
            }
            else
            {
                throw new TokenInvalidException();
            }



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                // Replace these values with your actual values
                ValidIssuer = _options.Issure,

                IssuerSigningKey = key,
                ValidAudience = _options.Issure,
            };

            // Create a token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Validate and read the token
                var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken validatedToken);

                // Extract user data from claims
                string userId = claimsPrincipal.FindFirst(Claims.UserId)?.Value;

                var userFound = _db.Users.FirstOrDefault(x => x.Id == userId);

                if (userFound != null)
                {

                    return userFound.Id;

                }
                else
                {
                    return "0";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                throw new TokenInvalidException();
            }


        }



        //public async Task<dynamic> CreateAdmin(CreateAdminDto dto)
        //{
        //    var checkuserEmail = _db.Users.Any(x => x.Email == dto.Email);
        //    var checkuserUsername = _db.Users.Any(x => x.UserName == dto.Username);
        //    var checkuserPhone = _db.Users.Any(x => x.PhoneNumber == dto.PhoneNumber);

        //    if (checkuserEmail)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.DuplicateEmail]);
        //    }
        //    if (!IsUsernameValid(dto.Username))
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.InvalidUsername]);
        //    }
        //    if (checkuserUsername)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.DuplicateUsername]);
        //    }

        //    if (checkuserPhone)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.DuplicatePhone]);
        //    }

        //    var user = _mapper.Map<User>(dto);
        //    Random random = new Random();
        //    var generatedOtp = random.Next(1000, 10000);
        //    user.OtpCode = generatedOtp.ToString();

        //    var generatedStudentNum = random.Next(10000, 100000);
        //    user.UserName = dto.Username;
        //    user.Name = dto.Username;
        //    user.PhoneNumber = dto.PhoneNumber;
        //    var city = await _db.Cities.FirstOrDefaultAsync();
        //    user.CityId = city.Id;
        //    var GeneratedPassword = GeneratePassword();
        //    user.UserType = UserType.Admin;

        //    try
        //    {
        //        var result = _userManager.CreateAsync(user, GeneratedPassword).GetAwaiter().GetResult();




        //        if (result.Succeeded)
        //        {
        //            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.InfoEmailSend], new
        //            {
        //                password = GeneratedPassword,
        //                username = user.UserName,
        //                phoneNumber = user.PhoneNumber,
        //            });
        //        }
        //        else
        //        {

        //            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //            var resultMsg = $"User creation failed. Errors: {errors}";
        //            return new ApiResponseFailedViewModel(resultMsg);


        //        }
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        // Check if the exception is related to a unique constraint violation
        //        if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
        //        {
        //            // Handle the duplicate key violation for the 'Email' column
        //            return new ApiResponseFailedViewModel("Email address is already in use. Please choose a different email.");
        //        }
        //        else
        //        {
        //            // Handle other types of DbUpdateException or rethrow the exception
        //            throw;
        //        }
        //    }


        //}
        //private string GeneratePassword()
        //{
        //    // Define the criteria for the password
        //    const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        //    const string numbers = "0123456789";
        //    const string symbols = "!@#$%^&*()_+";

        //    // Combine all characters for the password
        //    string allCharacters = uppercaseLetters + lowercaseLetters + numbers + symbols;

        //    // Use a random number generator to select characters for the password
        //    Random random = new Random();
        //    char[] passwordChars = new char[8];

        //    // Ensure at least one uppercase letter
        //    passwordChars[0] = uppercaseLetters[random.Next(uppercaseLetters.Length)];


        //    // Ensure at least one digit
        //    passwordChars[1] = numbers[random.Next(numbers.Length)];

        //    // Fill the remaining characters randomly
        //    for (int i = 2; i < passwordChars.Length; i++)
        //    {
        //        passwordChars[i] = allCharacters[random.Next(allCharacters.Length)];
        //    }

        //    // Shuffle the characters to make the password more random
        //    passwordChars = passwordChars.OrderBy(c => random.Next()).ToArray();

        //    // Convert the character array to a string
        //    string password = new string(passwordChars);

        //    return password;
        //}

        private bool IsUsernameValid(string username)
        {
            // Check if the username starts with a lowercase letter
            bool lowerCase = username.Any(char.IsLower);


            // Check if the username contains at least one digit (optional)
            //bool containsDigit = username.Any(char.IsDigit);

            // Check if the username has a minimum length of 5 characters
            bool hasMinLength = username.Length >= 5;

            // Return true only if all conditions are met
            return lowerCase && hasMinLength;
        }

        static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;

            int age = currentDate.Year - dateOfBirth.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }




    }




}
