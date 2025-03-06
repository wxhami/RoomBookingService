using MediatR;

namespace Application.Reservations.Command.AddReservation;

public class AddReservationCommand:IRequest<Guid>
{
    public Guid RoomId { get; set; }
    public Guid OrganizerId { get; set; }
    public DateTime StartReservation { get; set; }
    public DateTime EndReservation { get; set; }
    public string? Description { get; set; }
}