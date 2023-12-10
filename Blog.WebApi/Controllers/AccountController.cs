using Blog.Application.DTOs.Account;
using Blog.Application.Features.Account.Commands.EditUser;
using Blog.Application.Features.Account.Commands.Login;
using Blog.Application.Features.Account.Commands.Logout;
using Blog.Application.Features.Account.Commands.RefreshToken;
using Blog.Application.Features.Account.Commands.Register;
using Blog.Application.Features.Account.Queries.GetUser;
using Blog.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.WebApi.Extensions;

namespace Blog.WebApi.Controllers;

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
        var logoutCommand = new LogoutCommand(Request.BearerToken());
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

    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<TokenResponse>> Refresh(RefreshTokenRequest request)
    {
        var refreshTokenCommand = new RefreshTokenCommand(request);
        var tokenResponse = await Mediator.Send(refreshTokenCommand);
        return Ok(tokenResponse);
    }
}