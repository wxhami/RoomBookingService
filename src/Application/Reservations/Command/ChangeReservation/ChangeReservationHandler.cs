using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Command.ChangeReservation;

public class ChangeReservationHandler(IDatabaseContext databaseContext):IRequestHandler<ChangeReservationCommand>
{
    public async Task Handle(ChangeReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await databaseContext.Reservations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation == default) throw new ObjectNotFoundException();

        if (request.NewDescription != null)
        {
            reservation.EventDescription = request.NewDescription;
        }

        if (request.NewEndReservationTime != null)
        {
            reservation.EndReservation = (DateTime)request.NewEndReservationTime;
        }

        if (request.NewStartReservationTime != null)
        {
            reservation.StartReservation = (DateTime)request.NewStartReservationTime;
        }

       await databaseContext.SaveChangesAsync(cancellationToken);
    }
}