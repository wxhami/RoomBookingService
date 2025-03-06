using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Amenities.AddAmenity;

public class AddAmenityHandler(IDatabaseContext databaseContext): IRequestHandler<AddAmenityCommand, Guid>
{
    public async Task<Guid> Handle(AddAmenityCommand request, CancellationToken cancellationToken)
    {
      var amenity =  new Amenity()
            {
                Name = request.Name
            };

       databaseContext.Amenities.Add(amenity);
       await databaseContext.SaveChangesAsync(cancellationToken);

       return amenity.Id;
    }
}