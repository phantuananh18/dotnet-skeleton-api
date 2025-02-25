using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Users;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateUserCommand
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<CreateUserRequest>(command);
                return await _userService.CreateUserAsync(request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
