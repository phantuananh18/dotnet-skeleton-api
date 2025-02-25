
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.MessageModule.Domain.Interfaces.Services
{
    public interface IMessagingService
    {
        Task<BaseResponse> StartVerificationAsync(string phoneNumber, string channel);

        Task<BaseResponse> CheckVerificationAsync(string phoneNumber, string code);

        Task<BaseResponse> SendSmsAsync(string ToPhoneNumber, string message);
    }
}
