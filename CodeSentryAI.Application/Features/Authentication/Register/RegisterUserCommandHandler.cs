using CodeSentryAI.Application.Common.Exceptions;
using CodeSentryAI.Application.Interfaces.Authentication;
using CodeSentryAI.Application.Interfaces.Persistence;
using CodeSentryAI.Domain.Entities;
using MediatR;

namespace CodeSentryAI.Application.Features.Authentication.Register;

public sealed class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserResponse> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        // Check for duplicate email
        var emailExists = await _userRepository.ExistsByEmailAsync(
            request.Email,
            cancellationToken);

        if (emailExists)
        {
            throw new ConflictException("Email already exists.");
        }

        // Hash password
        var passwordHash =
            _passwordHasher.HashPassword(request.Password);

        // Create domain entity
        var user = new User(
            request.FullName,
            request.Email,
            passwordHash);

        // Persist
        await _userRepository.AddAsync(
            user,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        // Return response
        return new RegisterUserResponse(
            user.Id,
            user.FullName,
            user.Email);
    }
}
