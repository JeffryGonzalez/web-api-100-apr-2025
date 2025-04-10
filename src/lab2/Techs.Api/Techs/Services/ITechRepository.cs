

namespace Techs.Api.Techs.Services;

public interface ITechRepository
{
    Task<TechResponseModel> AddTechAsync(TechCreateModel request);
    Task<TechResponseModel?> GetTechByIdAsync(Guid id, CancellationToken token = default);
    Task<TechNameResponseModel?> GetTechNameBySubAsync(string sub, CancellationToken token = default);
}
