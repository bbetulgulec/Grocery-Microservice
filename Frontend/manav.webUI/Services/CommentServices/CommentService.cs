using manav.DtoLayer.CommentDtos.CommentDtos;

namespace manav.webUI.Services.CatalogServices.CommentServices;

public class CommentService:ICommentService
{
    private readonly HttpClient _httpClient;

    public CommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultCommentDto>> GetAllCommentAsync()
    {
        var responseMassage =   await _httpClient.GetAsync($"comment");
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultCommentDto>>();
        return values;
    }

    public async Task<UpdateCommentDto> GetByIdCommentAsync(int id)
    {
        var responseMassage = await _httpClient.GetAsync("comment/"+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<UpdateCommentDto>();
        return values;    }

    public async Task UpdateCommentDto(UpdateCommentDto updateCommentDto)
    {
        await _httpClient.PutAsJsonAsync<UpdateCommentDto>("comment", updateCommentDto);
    }

    public async Task CreateCommentDto(CreateCommentDto createCommentDto)
    {
        await  _httpClient.PostAsJsonAsync<CreateCommentDto>("comment", createCommentDto);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _httpClient.DeleteAsync($"comment?id={id}");
    }

    public async Task ChangeCommentStatusAsync(int id)
    {
        var response = await _httpClient.GetAsync(
            $"comment/ChangeCommentStatus/{id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task SyncGoogleReviewsAsync()
    {
        var response = await _httpClient.PostAsync("comment/FetchGoogleReviews", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<ResultCommentDto>> GetActiveCommentsAsync()
    {
        var responseMessage = await _httpClient.GetAsync("comment/getactivecomments");

        var values = await responseMessage.Content
            .ReadFromJsonAsync<List<ResultCommentDto>>();

        return values;
    }
}