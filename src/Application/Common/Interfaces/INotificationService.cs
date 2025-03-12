namespace Application.Common.Interfaces;

public interface INotificationService
{
    public void ScheduleNotification(Guid reservationId, DateTime startTime);
}