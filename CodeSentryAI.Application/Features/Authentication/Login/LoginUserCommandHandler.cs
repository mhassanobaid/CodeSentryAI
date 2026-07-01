using MediatR;
using CodeSentryAI.Application.Common.Exceptions;
using CodeSentryAI.Application.Interfaces.Authentication;
using CodeSentryAI.Application.Interfaces.Persistence;

namespace CodeSentryAI.Application.Features.Authentication.Login;

public sealed class LoginUserCommandHandler
    : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginUserResponse> Handle(
    LoginUserCommand request,
    CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (user is null)
            throw new NotFoundException("Invalid email or password.");

        var validPassword = _passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash);

        if (!validPassword)
            throw new NotFoundException("Invalid email or password.");

        var accessToken =
            _jwtTokenGenerator.GenerateAccessToken(user);

        var refreshToken =
            _jwtTokenGenerator.GenerateRefreshToken(user.Id);

        await _refreshTokenRepository.AddAsync(
            refreshToken,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new LoginUserResponse(
            user.Id,
            user.FullName,
            user.Email,
            accessToken,
            refreshToken.Token);
    }

}