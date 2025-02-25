using DotnetSkeleton.EmailModule.Application.Commands.QueueSendOutgoingEmailCommand;
using DotnetSkeleton.EmailModule.Application.Commands.SendOutgoingEmailCommand;
using DotnetSkeleton.EmailModule.Domain.Models.Requests;

namespace DotnetSkeleton.EmailModule.Application.Mappings;

public class MappingEmail : Profile
{
    public MappingEmail()
    {
        CreateMap<SendOutgoingEmailCommand, OutgoingEmailRequest>()
            .ReverseMap();

        CreateMap<QueueSendOutgoingEmailCommand, OutgoingEmailRequest>()
            .ReverseMap();
    }
}