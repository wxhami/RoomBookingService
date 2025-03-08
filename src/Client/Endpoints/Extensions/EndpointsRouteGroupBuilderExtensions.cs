namespace Client.Endpoints.Extensions;

public static class EndpointsRouteGroupBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder) =>
        builder.MapGroup("api/v1").MapRooms().MapAmenities().MapUsers().MapReservations();
}
