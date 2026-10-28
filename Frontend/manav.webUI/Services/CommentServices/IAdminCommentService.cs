using manav.DtoLayer.CommentDtos.CommentDtos;

namespace manav.webUI.Services.CatalogServices.CommentServices;

public interface IAdminCommentService
{
    Task<List<ResultCommentDto>> GetAllCommentAsync();
    Task<UpdateCommentDto> GetByIdCommentAsync(int id);
    Task UpdateCommentDto(UpdateCommentDto updateCommentDto);
    Task CreateCommentDto(CreateCommentDto createCommentDto);
    Task DeleteCommentAsync(int id);
    Task ChangeCommentStatusAsync(int id);
    Task SyncGoogleReviewsAsync();
}