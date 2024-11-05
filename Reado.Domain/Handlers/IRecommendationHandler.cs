using Reado.Domain.Entities;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;

namespace Reado.Domain.Handlers;

public interface IRecommendationHandler
{
    Task<Response<Recommendation?>> CreateAsync(CreateRecommendationRequest request);
    Task<Response<Recommendation?>> DeleteAsync(DeleteRecommendationRequest request);
    Task<PageResponse<List<Recommendation>>> GetByUserIdAsync(GetRecommendationByUserIdRequest request);
    Task<Response<Recommendation?>> GetByIdAsync(GetRecommendationByIdRequest request);
    Task<Response<Recommendation?>> UpdateAsync(UpdateRecommendationRequest request);
}
