using CodeSentryAI.Application.Features.Authentication.Login;
using CodeSentryAI.Application.Features.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeSentryAI.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(Register),
            new { id = response.UserId },
            response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _sender.Send(command, cancellationToken);

        return Ok(response);
    }
}