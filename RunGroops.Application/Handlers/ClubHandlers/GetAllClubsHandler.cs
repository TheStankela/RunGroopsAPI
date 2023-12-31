﻿using MediatR;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class GetAllClubsHandler : IRequestHandler<GetAllClubsQuery, PagedList<Club>>
    {
        private readonly IClubRepository _clubRepository;
        public GetAllClubsHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<PagedList<Club>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Club> query = await _clubRepository.GetClubsAsync();

            return await PagedList<Club>.CreateAsync(query, request.PageNumber, request.PageSize);
        }
    }
}
