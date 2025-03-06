using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservations.Query.GetReservationById;

public class GetReservationByIdHandler(IDatabaseContext databaseContext): IRequestHandler<GetReservationByIdQuery, Reservation>
{
    public async Task<Reservation> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await databaseContext.Reservations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (reservation == default) throw new ObjectNotFoundException(); 

        return reservation;
    }
}