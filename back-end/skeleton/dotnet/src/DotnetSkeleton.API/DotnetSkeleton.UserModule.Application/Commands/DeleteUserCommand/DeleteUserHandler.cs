using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteUserCommand
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _userService.DeleteUserAsync(command.UserId);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
