using Infrastructure.Persistence;

namespace Client.Endpoints.Extensions;

public static class EndpointsRouteGroupBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder) =>
        builder.MapGroup("api/v1").MapRooms().MapAmenities().MapUsers().MapReservations().MapGroup("api/v1/identity/")
            .MapIdentityApi<ApplicationUser>();
}