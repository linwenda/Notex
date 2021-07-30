using System.Threading;
using System.Threading.Tasks;
using MarchNote.Application.Configuration.Commands;
using MarchNote.Application.Configuration.Exceptions;
using MarchNote.Application.Configuration.Responses;
using MarchNote.Domain;
using MarchNote.Domain.SeedWork;
using NUnit.Framework;
using Shouldly;

namespace MarchNote.IntegrationTests.Behaviors
{
    using static TestFixture;

    public class ResponseBehaviorTest : TestBase
    {
        [Test]
        public async Task ShouldReturnNoteResponse()
        {
            var smartNoteResponse = await Send(new PingCommand(DefaultResponseCode.Succeeded));
            smartNoteResponse.Code.ShouldBe(DefaultResponseCode.Succeeded);
            
            smartNoteResponse = await Send(new PingCommand(DefaultResponseCode.Invalid));
            smartNoteResponse.Code.ShouldBe(DefaultResponseCode.Invalid);
            
            smartNoteResponse = await Send(new PingCommand(DefaultResponseCode.Forbidden));
            smartNoteResponse.Code.ShouldBe(DefaultResponseCode.Forbidden);
            
            smartNoteResponse = await Send(new PingCommand(DefaultResponseCode.NotFound));
            smartNoteResponse.Code.ShouldBe(DefaultResponseCode.NotFound);
            
            smartNoteResponse = await Send(new PingCommand((int)ExceptionCode.BusinessCheckFailed));
            smartNoteResponse.Code.ShouldBe((int)ExceptionCode.BusinessCheckFailed);
        }
        
        public class PingCommand : ICommand<MarchNoteResponse>
        {
            public PingCommand(int code)
            {
                Code = code;
            }

            public int Code { get;  }
        }

        public class PingCommandHandler : ICommandHandler<PingCommand, MarchNoteResponse>
        {
            public Task<MarchNoteResponse> Handle(PingCommand request, CancellationToken cancellationToken)
            {
                if (request.Code == DefaultResponseCode.Invalid)
                {
                    throw new ValidationException();
                }
                
                if (request.Code == DefaultResponseCode.Forbidden)
                {
                    throw new ForbiddenException();
                }

                if (request.Code == DefaultResponseCode.NotFound)
                {
                    throw new NotFoundException();
                }

                if (request.Code == (int) ExceptionCode.BusinessCheckFailed)
                {
                    throw new BusinessException(ExceptionCode.BusinessCheckFailed, "");
                }

                return Task.FromResult(new MarchNoteResponse());
            }
        }
    }
}