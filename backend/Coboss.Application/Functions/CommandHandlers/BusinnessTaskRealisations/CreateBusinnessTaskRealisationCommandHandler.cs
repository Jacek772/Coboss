using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Services;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.Exceptions;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTaskRealisations
{
    public class CreateBusinnessTaskRealisationCommandHandler : IRequestHandler<CreateBusinnessTaskRealisationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IBusinnessTasksService _businnessTasksService;

        public CreateBusinnessTaskRealisationCommandHandler(IMapper mapper,
            IBusinnessTasksService businnessTasksService)
        {
            _mapper = mapper;
            _businnessTasksService = businnessTasksService;
        }

        public async Task<Unit> Handle(CreateBusinnessTaskRealisationCommand request, CancellationToken cancellationToken)
        {
            BusinnessTask businnessTask = await _businnessTasksService.GetBusinnessTasksAsync(request.TaskId);
            if (businnessTask == null)
            {
                throw new BadRequestException($"BusinnessTask with id = {request.TaskId} cannot exists.");
            }

            BusinnessTaskRealisation businnessTaskRealisation = _mapper.Map<CreateBusinnessTaskRealisationCommand, BusinnessTaskRealisation>(request);
            businnessTask.TaskRealisations.Add(businnessTaskRealisation);
            await _businnessTasksService.UpdateBusinnessTaskAsync(businnessTask);
            return Unit.Value;
        }
    }
}
