namespace Techs.Api.Techs.Services;

public interface ITechRepository
{
    Task<TechResponseModel> AddTechAsync(TechCreateModel request);
    Task<TechResponseModel?> GetTechByIdAsync(Guid id, CancellationToken token = default);

    Task<List<TechResponseModel?>> GetAllTechsAsync();
    Task<List<TechResponseModel?>> GetTechsBySubAsync(string sub);
}
