using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Account.Commands.EditUser;

public sealed class EditUserCommandHandler : IRequestHandler<EditUserCommand>
{
    private readonly IUserRepository _userRepository;
    
    public EditUserCommandHandler(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null) throw new NotFoundException(nameof(User), request.Id);

        user.Email = request.EditModel.Email;
        user.FullName = request.EditModel.FullName;
        user.BirthDate = request.EditModel.BirthDate;
        user.Gender = request.EditModel.Gender;
        user.PhoneNumber = request.EditModel.PhoneNumber;

        await _userRepository.UpdateAsync(user);
    }
}