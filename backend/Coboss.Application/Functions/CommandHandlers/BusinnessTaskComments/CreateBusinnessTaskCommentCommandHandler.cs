using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.Exceptions;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTaskComments
{
    public class CreateBusinnessTaskCommentCommandHandler : IRequestHandler<CreateBusinnessTaskCommentCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IBusinnessTasksService _businnessTasksService;
        private readonly IUsersService _usersService;

        public CreateBusinnessTaskCommentCommandHandler(IMapper mapper,
            IBusinnessTasksService businnessTasksService, IUsersService usersService)
        {
            _mapper = mapper;
            _businnessTasksService = businnessTasksService;
            _usersService = usersService;
        }

        public async Task<Unit> Handle(CreateBusinnessTaskCommentCommand request, CancellationToken cancellationToken)
        {
            BusinnessTask businnessTask = await _businnessTasksService.GetBusinnessTasksAsync(request.TaskId);
            if(businnessTask == null)
            {
                throw new BadRequestException($"BusinnessTask with id = {request.TaskId} cannot exists.");
            }

            User user = await _usersService.GetByIdAsync(request.UserId);
            if(user == null)
            {
                throw new BadRequestException($"User with id = {request.UserId} cannot exists.");
            }

            BusinnessTaskComment businnessTaskComment = _mapper.Map<CreateBusinnessTaskCommentCommand, BusinnessTaskComment>(request);
            businnessTaskComment.User = user;
            businnessTask.Comments.Add(businnessTaskComment);
            await _businnessTasksService.UpdateBusinnessTaskAsync(businnessTask);
            return Unit.Value;
        }
    }
}
