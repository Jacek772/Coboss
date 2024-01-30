using AutoMapper;
using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Coboss.Types.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTasks
{
    public class CreateBusinnessTaskCommandHandler : IRequestHandler<CreateBusinnessTaskCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IProjectsService _projectsService;
        private readonly ApplicationDbContext _applicationDbContext;

        public CreateBusinnessTaskCommandHandler(IMapper mapper,
            ApplicationDbContext applicationDbContext,
            IBusinnessTasksService businnessTasksService,
            IProjectsService projectsService)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _projectsService = projectsService;
        }

        public async Task<Unit> Handle(CreateBusinnessTaskCommand request, CancellationToken cancellationToken)
        {
            Project project = await _projectsService.GetByIdAsync(request.ProjectId);
            if (project == null)
            {
                throw new BadRequestException($"Project with id = {request.ProjectId} cannot exists.");
            }
            BusinnessTask businnessTask = _mapper.Map<CreateBusinnessTaskCommand, BusinnessTask>(request);

            if(request.Comments?.Length > 0)
            {
                List<BusinnessTaskComment> comments = new List<BusinnessTaskComment>();
                foreach(CreateBusinnessTaskCommentCommand commentCommand in request.Comments)
                {
                    BusinnessTaskComment businnessTaskComment = new BusinnessTaskComment
                    {
                        Text = commentCommand.Text,
                        Date = commentCommand.Date,
                        User = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == commentCommand.UserId)
                    };
                    comments.Add(businnessTaskComment);
                }

                businnessTask.Comments = comments;
            }

            project.BusinnessTasks.Add(businnessTask);
            await _projectsService.UpdateAsync(project);
            return Unit.Value;
        }
    }
}
