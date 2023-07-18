using MediatR;
using RunGroops.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroops.Application.Queries.UserQueries
{
    public record GetUsersQuery(int PageNumber) : IRequest<ICollection<UserResponse>>;
}
