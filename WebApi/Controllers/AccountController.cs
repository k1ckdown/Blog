using Application.DTOs.Account;
using Application.Features.Account.Commands.EditUser;
using Application.Features.Account.Commands.Login;
using Application.Features.Account.Commands.Logout;
using Application.Features.Account.Commands.Register;
using Application.Features.Account.Queries.GetUser;
using Application.Wrappers;
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
    public async Task<ActionResult<TokenResponse>> Register(UserRegisterModel registerModel)
    {
        var registerCommand = new RegisterCommand(registerModel);
        var tokenResponse = await Mediator.Send(registerCommand);
        return Ok(tokenResponse);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenResponse>> LogIn(LoginCredentials credentials)
    {
        var loginCommand = new LoginCommand(credentials);
        var tokenResponse = await Mediator.Send(loginCommand);
        return Ok(tokenResponse);
    }
    
    [HttpPost]
    [Route("logout")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<Response>> LogOut()
    {
        var logoutCommand = new LogoutCommand();
        var response = await Mediator.Send(logoutCommand);
        return Ok(response);
    }

    [HttpGet]
    [Route("profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        var getUserQuery = new GetUserQuery(UserId);
        var userDto = await Mediator.Send(getUserQuery);
        return Ok(userDto);
    }

    [HttpPut]
    [Route("profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EditProfile(UserEditModel editModel)
    {
        var editUserCommand = new EditUserCommand(UserId, editModel);
        await Mediator.Send(editUserCommand);
        return Ok();
    }
}