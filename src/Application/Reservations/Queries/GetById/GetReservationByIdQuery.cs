using Domain.Entities;
using MediatR;

namespace Application.Reservations.Queries.GetById;

public class GetReservationByIdQuery : IRequest<Reservation>
{
    public Guid Id { get; set; }
}