using Blog.Application.DTOs.Account;
using Blog.Application.Features.Account.Commands.Login;
using Blog.Application.Features.Account.Commands.Register;
using Blog.Application.Features.Account.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

public sealed class AccountController : BaseController
{
    public AccountController(IMediator mediator) : base(mediator) {}

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LogIn(LoginCredentials credentials)
    {
        var command = new LoginCommand(credentials);
        var tokenResponse = await Mediator.Send(command);
        return Ok(tokenResponse);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserRegisterModel registerModel)
    {
        var command = new RegisterCommand(registerModel);
        var tokenResponse = await Mediator.Send(command);
        return Ok(tokenResponse);
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