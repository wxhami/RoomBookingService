using Domain.Entities;
using MediatR;

namespace Application.Reservations.Query.GetReservationById;

public class GetReservationByIdQuery: IRequest<Reservation>
{
    public Guid Id { get; set; }
}