using Application.Common.Interfaces;
using Application.Common.Paging;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Users.Queries.GetAll;

public class GetAllUsersQuery : IRequest<PagedResult<ApplicationUser>>, IPagedQuery
{
    public int? PageSize { get; set; }
    public int? PageNumber { get; set; }
}