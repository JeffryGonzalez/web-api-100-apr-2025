

using Marten.Pagination;

namespace Techs.Api.Techs.Services;

public interface ITechRepository
{
    Task<TechResponseModel> AddTechAsync(TechCreateModel request);
    Task<TechResponseModel?> GetTechByIdAsync(Guid id, CancellationToken token = default);
    Task<TechResponseModel?> GetSoftwareTechBySubAsync(string sub, CancellationToken token = default);
    Task<IPagedList<TechResponseModel>> GetSoftwareTechsAsync(int pageNumber, int pageSize, CancellationToken token);
}
