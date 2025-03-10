using Application.Common.Interfaces;
using System.Timers;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Hangfire;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace Client.Services;

  public class NotificationService(IDatabaseContext databaseContext, IMailSender mailSender, UserManager<ApplicationUser> userManager) : INotificationService
    {
        public void ScheduleNotification(Guid reservationId, DateTime startTime)
        {
            var notifyTime = startTime.AddHours(-3);
            BackgroundJob.Schedule(() => SendNotificationAsync(reservationId), notifyTime);
        }

        private async void SendNotificationAsync(Guid reservationId)
        {
            var reservation = await databaseContext.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId) ??
                              throw new ObjectNotFoundException();
            var user = userManager.Users.FirstOrDefault(x => x.Id == reservation.OrganizerId) ??
                              throw new ObjectNotFoundException();
            await mailSender.SendMessageAsync(user.Email!, MessageConstants.Subject, MessageConstants.MessageText );
        }

    }