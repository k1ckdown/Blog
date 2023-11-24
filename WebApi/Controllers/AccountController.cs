using Application.DTOs.Account;
using Application.Features.Account.Commands.Login;
using Application.Features.Account.Commands.Logout;
using Application.Features.Account.Commands.Register;
using Application.Features.Account.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public sealed class AccountController : BaseController
{
    public AccountController(IMediator mediator) : base(mediator) {}

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegisterModel registerModel)
    {
        var command = new RegisterCommand(registerModel);
        var tokenResponse = await Mediator.Send(command);
        return Ok(tokenResponse);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LogIn(LoginCredentials credentials)
    {
        var command = new LoginCommand(credentials);
        var tokenResponse = await Mediator.Send(command);
        return Ok(tokenResponse);
    }
    
    [HttpPost]
    [Route("logout")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> LogOut()
    {
        var command = new LogoutCommand();
        var response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    [Route("profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetProfile()
    {
        var query = new GetUserQuery(UserId);
        var userDto = await Mediator.Send(query);
        return Ok(userDto);
    }
}