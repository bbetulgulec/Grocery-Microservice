using manav.DtoLayer.CommentDtos.CommentDtos;

namespace manav.webUI.Services.CatalogServices.CommentServices;

public class AdminCommentService : IAdminCommentService
{
    private readonly HttpClient _httpClient;

    public AdminCommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultCommentDto>> GetAllCommentAsync()
    {
        var response = await _httpClient.GetAsync("comment");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ResultCommentDto>>();
    }

    public async Task<UpdateCommentDto> GetByIdCommentAsync(int id)
    {
        var response = await _httpClient.GetAsync("comment/" + id);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UpdateCommentDto>();
    }

    public async Task UpdateCommentDto(UpdateCommentDto dto)
    {
        await _httpClient.PutAsJsonAsync("comment", dto);
    }

    public async Task CreateCommentDto(CreateCommentDto dto)
    {
        await _httpClient.PostAsJsonAsync("comment", dto);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _httpClient.DeleteAsync($"comment?id={id}");
    }

    public async Task ChangeCommentStatusAsync(int id)
    {
        var response = await _httpClient.GetAsync($"comment/ChangeCommentStatus/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task SyncGoogleReviewsAsync()
    {
        var response = await _httpClient.PostAsync("comment/FetchGoogleReviews", null);
        response.EnsureSuccessStatusCode();
    }
}