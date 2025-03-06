using MediatR;

namespace Application.Reservations.Command.AddReservation;

public class AddReservationCommand:IRequest<Guid>
{
    public Guid RoomId { get; set; }
    public string OrganizerId { get; set; } = null!;
    public DateTime StartReservation { get; set; }
    public DateTime EndReservation { get; set; }
    public string? Description { get; set; }
}