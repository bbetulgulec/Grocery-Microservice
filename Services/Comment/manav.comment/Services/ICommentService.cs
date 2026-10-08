using manav.component.Dtos;
using Microsoft.EntityFrameworkCore;

namespace manav.component.Services;

public interface ICommentService
{
    
    Task<List<ResultCommentDto>> GetAllCommentAsync();
    Task<GetByIdCommentDto> GetByIdCommentAsync(int id);
    Task UpdateCommentDto(UpdateCommentDto updateCommentDto);
    Task CreateCommentDto(CreateCommentDto createCommentDto);
    Task DeleteCommentAsync(int id);
    Task ChangeCommentStatusAsync(int id);
    Task SyncGoogleReviewsAsync();
    Task<List<ResultCommentDto>> GetActiveCommentsAsync();
}