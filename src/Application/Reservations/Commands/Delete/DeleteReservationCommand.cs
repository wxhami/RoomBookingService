using MediatR;

namespace Application.Reservations.Commands.Delete;

public class DeleteReservationCommand : IRequest
{
    public Guid Id { get; set; }
}