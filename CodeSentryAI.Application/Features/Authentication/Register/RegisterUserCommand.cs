using MediatR;

namespace CodeSentryAI.Application.Features.Authentication.Register;

/// <summary>
/// Command to register a new user.
/// </summary>
/*

Command

↓

Handler

↓

Repository Interface

↓

Infrastructure

↓

EF Core 
The handler doesn't care whether the data is stored in SQL Server, PostgreSQL, or even an in-memory database

 */

public sealed record RegisterUserCommand
(
    string FullName,
    string Email,
    string Password
)
    : IRequest<RegisterUserResponse>;
