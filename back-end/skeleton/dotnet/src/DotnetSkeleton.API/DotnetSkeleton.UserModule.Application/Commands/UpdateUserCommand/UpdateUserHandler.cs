using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Users;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateUserCommand
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {

            try
            {
                var request = _mapper.Map<UpdateUserRequest>(command);
                return await _userService.UpdateUserAsync(command.UserId, request);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
