using AutoMapper;
using Coboss.Application.Functions.Query.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers.BusinnessTasks
{
    public class GetBusinnessTasksQueryHandler : IRequestHandler<GetBusinnessTasksQuery, List<BusinnessTaskDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinnessTasksService _businnessTasksService;

        public GetBusinnessTasksQueryHandler(IMapper mapper,
            IBusinnessTasksService businnessTasksService)
        {
            _mapper = mapper;
            _businnessTasksService = businnessTasksService;
        }

        public async Task<List<BusinnessTaskDTO>> Handle(GetBusinnessTasksQuery request, CancellationToken cancellationToken)
        {
            List<BusinnessTask> businnessTasks = await _businnessTasksService.GetBusinnessTasksAsync(request);
            return _mapper.Map<List<BusinnessTask>, List<BusinnessTaskDTO>>(businnessTasks);
        }
    }
}
