using Appointment.API.Migrations;
using Common.Abstractions.IntegrationEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Appointment.API.UseCases.Event
{
    public class ChangeStatusToDBHandler : IRequestHandler<DomainEvent.ChangeStatusEvent>
    {
        private AppointServiceDbContext _dbContext;

        public ChangeStatusToDBHandler(AppointServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DomainEvent.ChangeStatusEvent request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.UserAppointInfors.FirstOrDefaultAsync(x => x.Id == request.IdApoint);
            entity.Status = request.Status;
            entity.Note = request.Note;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
