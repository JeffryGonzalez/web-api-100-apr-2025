﻿
using Marten;

namespace Techs.Api.Techs.Services;

public class PostGresMartenTechRepository(IDocumentSession session) : ITechRepository
{
    public async Task<TechResponseModel> AddTechAsync(TechCreateModel request)
    {
        var response = request.MapToResponse();
        var entity = response.MapToEntity();
        session.Store(entity);
        await session.SaveChangesAsync();
        return response;
    }

    public async Task<TechResponseModel?> GetTechByIdAsync(Guid id, CancellationToken token)
    {
        var response = await session.Query<TechEntity>()
            .Where(t => t.Id == id)
            .ProjectToResponse()
            .SingleOrDefaultAsync(token);
        return response;
    }

    public async Task<TechNameResponseModel?> GetTechNameBySubAsync(string sub, CancellationToken token)
    {
        var response = await session.Query<TechEntity>()
            .Where(t => t.Sub == sub)
            .ProjectToResponse()
            .SingleOrDefaultAsync(token);

        if (response == null)
        {
            return null;
        }

        return new TechNameResponseModel($"{response.FirstName} {response.LastName}");
    }
}
