
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

    public async Task<TechResponseModel?> GetTechByIdAsync(Guid id)
    {
        var response = await session.Query<TechEntity>()
            .Where(t => t.Id == id)
            .ProjectToResponse()
            .SingleOrDefaultAsync();
        return response;
    }
}
