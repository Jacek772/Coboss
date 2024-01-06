using AutoMapper;
using Coboss.Application.Functions.Query.BusinnessTaskComments;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.QueryHandlers.BusinnessTaskComments
{
    public class GetBusinnessTaskCommentsQueryHandler : IRequestHandler<GetBusinnessTaskCommentsQuery, List<BusinnessTaskCommentDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IBusinnessTaskCommentsService _businnessTaskCommentsService;

        public GetBusinnessTaskCommentsQueryHandler(IMapper mapper,
            IBusinnessTaskCommentsService businnessTaskCommentsService)
        {
            _mapper = mapper;
            _businnessTaskCommentsService = businnessTaskCommentsService;
        }

        public async Task<List<BusinnessTaskCommentDTO>> Handle(GetBusinnessTaskCommentsQuery request, CancellationToken cancellationToken)
        {
            List<BusinnessTaskComment> businnessTaskComments = await _businnessTaskCommentsService.GetAsync(request);
            return _mapper.Map<List<BusinnessTaskComment>, List<BusinnessTaskCommentDTO>>(businnessTaskComments);
        }
    }
}
