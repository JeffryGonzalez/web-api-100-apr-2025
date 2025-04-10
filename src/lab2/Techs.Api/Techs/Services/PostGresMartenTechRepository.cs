
using Marten;
using Marten.Pagination;
using Microsoft.AspNetCore.SignalR;
using RTools_NTS.Util;

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

    public async Task<TechResponseModel?> GetSoftwareTechBySubAsync(string sub, CancellationToken token)
    {
        var response = await session.Query<TechEntity>().Where(t => t.Sub == sub).ProjectToResponse().FirstOrDefaultAsync(token);
        return response;
    }

    public async Task<IPagedList<TechResponseModel>> GetSoftwareTechsAsync(int pageNumber, int pageSize, CancellationToken token)
    {
        var response = await session.Query<TechEntity>().ProjectToResponse().ToPagedListAsync(pageNumber, pageSize, token);
        return response;
    }
}
