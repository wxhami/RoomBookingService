using MediatR;

namespace Application.Reservations.Command.DeleteReservation;

public class DeleteReservationCommand : IRequest
{
    public Guid Id { get; set; }
}