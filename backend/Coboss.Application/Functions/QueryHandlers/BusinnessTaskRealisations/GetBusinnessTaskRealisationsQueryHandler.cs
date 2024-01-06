using AutoMapper;
using Coboss.Application.Functions.Query.BusinnessTaskRealisations;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers.BusinnessTaskRealisations
{
    public class GetBusinnessTaskRealisationsQueryHandler : IRequestHandler<GetBusinnessTaskRealisationsQuery, List<BusinnessTaskRealisationDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinnessTaskRealisationsService _businnessTaskRealisationsService;

        public GetBusinnessTaskRealisationsQueryHandler(IMapper mapper,
            IBusinnessTaskRealisationsService businnessTaskRealisationsService)
        {
            _mapper = mapper;
            _businnessTaskRealisationsService = businnessTaskRealisationsService;
        }

        public async Task<List<BusinnessTaskRealisationDTO>> Handle(GetBusinnessTaskRealisationsQuery request, CancellationToken cancellationToken)
        {
            List<BusinnessTaskRealisation> businnessTaskRealisations = await _businnessTaskRealisationsService.GetAsync(request);
            return _mapper.Map<List<BusinnessTaskRealisation>, List<BusinnessTaskRealisationDTO>>(businnessTaskRealisations);
        }
    }
}
