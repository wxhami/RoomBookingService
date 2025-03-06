using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Command.DeleteReservation;

public class DeleteReservationHandler(IDatabaseContext databaseContext): IRequestHandler<DeleteReservationCommand>
{
    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await databaseContext.Reservations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation == null) throw new ObjectNotFoundException();

        databaseContext.Reservations.Remove(reservation);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}