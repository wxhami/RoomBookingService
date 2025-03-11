using MediatR;

namespace Application.Reservations.Commands.Change;

public class ChangeReservationCommand : IRequest
{
    public Guid Id { get; set; }
    public string? NewDescription { get; set; }
    public DateTime? NewStartReservationTime { get; set; }
    public DateTime? NewEndReservationTime { get; set; }
}