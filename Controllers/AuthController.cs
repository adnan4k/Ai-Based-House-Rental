using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HouseStoreApi.Configuration;
using HouseStoreApi.Services;
using HouseStoreApi.Models;
using System.Security.Claims;

namespace HouseStoreApi.controllers;

[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase {

    public static CustomerRegisterDto user=new CustomerRegisterDto();
    private readonly ILogger<AuthController> _logger;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly CustomersService _customerServices;

    // private List<User> _users= new List<User> {new User{Id = "124", Name = "Getu" }};

    public AuthController(ILogger<AuthController> logger, IOptions<JwtConfiguration> jwtConfiguration, CustomersService customerService)
    {
        _logger = logger;
        _jwtConfiguration = jwtConfiguration.Value;
        _customerServices = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Customer>>> Get() { return Ok(await _customerServices.GetAsync());}

    [HttpPost("login")]
     public IActionResult Login([FromBody] LoginDto request){
        if(user.email!=request.email){
            return BadRequest("user is not found");}
        return Ok("successfull");
      
    //      if( user!=null)
    //      {  
    //     var token = GenerateToken(user);
    //     return Ok(token);
    //     }
    //     return NotFound("user is not found");
     }
    
   
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterDto userDto){
        var user = new Customer { fullName = userDto.fullName, phone = userDto.phone, email = userDto.email, password = userDto.Password};
        await _customerServices.CreateAsync(user);
        var token = GenerateToken(user);
        return CreatedAtAction(nameof(Get), new {token = token}, user);
    }
    private string GenerateToken(Customer user){
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
        var tokenDesription = new SecurityTokenDescriptor(){
            Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id)}),
            Expires = DateTime.Now.AddDays(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDesription);
        return tokenHandler.WriteToken(token);
    }
}