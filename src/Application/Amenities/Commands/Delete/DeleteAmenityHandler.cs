﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Amenities.Commands.Delete;

public class DeleteAmenityHandler(IDatabaseContext databaseContext) : IRequestHandler<DeleteAmenityCommand>
{
    public async Task Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = await databaseContext.Amenities.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (amenity == null) throw new ObjectNotFoundException();

        databaseContext.Amenities.Remove(amenity);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}