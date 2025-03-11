using Application.Common.Interfaces;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Users.Queries.GetAll;

public class GetAllUsersQuery : IRequest<List<ApplicationUser>>, IPagedQuery
{
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}