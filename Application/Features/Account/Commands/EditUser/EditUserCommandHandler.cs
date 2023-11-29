using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using MediatR;

namespace Application.Features.Account.Commands.EditUser;

public sealed class EditUserCommandHandler : IRequestHandler<EditUserCommand>
{
    private readonly IAccountService _accountService;
    private readonly IUserRepository _userRepository;
    
    public EditUserCommandHandler(IAccountService accountService, IUserRepository userRepository)
    {
        _accountService = accountService;
        _userRepository = userRepository;
    }

    public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        user.Email = request.EditModel.Email;
        user.FullName = request.EditModel.FullName;
        user.BirthDate = request.EditModel.BirthDate;
        user.Gender = request.EditModel.Gender;
        user.PhoneNumber = request.EditModel.PhoneNumber;

        await _userRepository.UpdateAsync(user);
        await _accountService.UpdateUser(request.Id, request.EditModel.Email);
    }
}