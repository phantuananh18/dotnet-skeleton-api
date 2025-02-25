using DotnetSkeleton.EmailModule.Domain.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.EmailModule.Application.Commands.QueueSendOutgoingEmailCommand;

public class QueueSendOutgoingEmailHandler : IRequestHandler<QueueSendOutgoingEmailCommand, BaseResponse>
{
    private readonly Domain.Interfaces.Services.IEmailService _emailService;
    private readonly IMapper _mapper;

    public QueueSendOutgoingEmailHandler(Domain.Interfaces.Services.IEmailService emailService, IMapper mapper)
    {
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(QueueSendOutgoingEmailCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<OutgoingEmailRequest>(request);
        return await _emailService.QueueOutgoingEmailHandlerAsync(data);
    }
}