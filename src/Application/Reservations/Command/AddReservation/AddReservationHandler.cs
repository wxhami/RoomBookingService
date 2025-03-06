using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Reservations.Command.AddReservation;

public class AddReservationHandler(IDatabaseContext databaseContext): IRequestHandler<AddReservationCommand, Guid>
{
    public async Task<Guid> Handle(AddReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new Reservation()
        {
            RoomId = request.RoomId,
            OrganizerId = request.OrganizerId,
            StartReservation = request.StartReservation,
            EndReservation = request.EndReservation,
            EventDescription = request.Description
        };

        databaseContext.Reservations.Add(reservation);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return reservation.Id;
    }
}