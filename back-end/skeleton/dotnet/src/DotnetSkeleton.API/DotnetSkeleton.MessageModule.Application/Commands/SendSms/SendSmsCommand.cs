using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.MessageModule.Application.Commands.SendSms
{
    public class SendSmsCommand : IRequest<BaseResponse>
    {
        public required string ToMobilePhone { get; set; }
        public required string Message { get; set; }
    }
}
