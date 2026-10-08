using AutoMapper;
using manav.component.Context;
using manav.component.Dtos;
using manav.component.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace manav.component.Services;

public class CommentService:ICommentService
{
    private readonly CommentContext _context;
    private readonly IMapper _mapper;

    public CommentService(CommentContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ResultCommentDto>> GetAllCommentAsync()
    { 
        var values = await _context.Comments.ToListAsync();
        return _mapper.Map<List<ResultCommentDto>>(values);
    }

    public async Task<GetByIdCommentDto> GetByIdCommentAsync(int id)
    {
        var value = await _context.Comments.FindAsync(id);
        return _mapper.Map<GetByIdCommentDto>(value);
    }

    public async Task UpdateCommentDto(UpdateCommentDto updateCommentDto)
    {
        var value = _mapper.Map<UserComment>(updateCommentDto);
        _context.Comments.Update(value);
        await _context.SaveChangesAsync();    }

    public async Task CreateCommentDto(CreateCommentDto createCommentDto)
    {
        var value = _mapper.Map<UserComment>(createCommentDto);
        await _context.Comments.AddAsync(value);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int id)
    {
        var value = await _context.Comments.FindAsync(id);
        _context.Comments.Remove(value);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeCommentStatusAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id); 
        if (comment != null)
        {
            comment.status = !comment.status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task SyncGoogleReviewsAsync()
    {
        string apiKey = "AIzaSyA9wslxg-88KrLS7UgpTQX-xIsnr1ovepc";
        string placeId = "ChIJF05-oZiwyhQRv5TANBZ7M_I";
        string url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=reviews&language=tr&key={apiKey}";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return;

        var jsonString = await response.Content.ReadAsStringAsync();
        var googleData = JsonConvert.DeserializeObject<ResponseGoogleReviewDto>(jsonString);

        if (googleData?.Result?.Reviews == null || !googleData.Result.Reviews.Any()) return;

        var reviews = googleData.Result.Reviews;

        var activeGoogleAuthors = reviews
            .Select(r => r.Author_Name.Trim())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        var existingComments = await _context.Comments.ToListAsync();
        var toUnverify = existingComments.Where(x => x.isAddedGoogle 
                                                     && !activeGoogleAuthors.Contains($"{x.FirstName.Trim()} {x.LastName.Trim()}".Trim()));

        foreach (var dbComment in toUnverify)
        {
            dbComment.isAddedGoogle = false;
        }
        foreach (var review in reviews)
        {
            string fullAuthorName = review.Author_Name.Trim();

            var matchedLocalComment = existingComments.FirstOrDefault(x =>
                $"{x.FirstName.Trim()} {x.LastName.Trim()}".Equals(fullAuthorName, StringComparison.OrdinalIgnoreCase) &&
                !x.isAddedGoogle);

            if (matchedLocalComment != null)
            {
                matchedLocalComment.isAddedGoogle = true;
                if (!string.IsNullOrEmpty(review.Profile_Photo_Url))
                {
                    matchedLocalComment.UserImageURL = review.Profile_Photo_Url;
                }
            }
            else
            {
                bool isAlreadyInDb = existingComments.Any(x =>
                    $"{x.FirstName.Trim()} {x.LastName.Trim()}".Equals(fullAuthorName, StringComparison.OrdinalIgnoreCase) &&
                    x.isAddedGoogle);

                if (!isAlreadyInDb)
                {
                    var nameParts = fullAuthorName.Split(' ', 2);
                    var newComment = new UserComment
                    {
                        FirstName = nameParts[0],
                        LastName = nameParts.Length > 1 ? nameParts[1] : "",
                        UserImageURL = review.Profile_Photo_Url,
                        CommentDetail = review.Text,
                        Rating = review.Rating,
                        CreatedDate = DateTimeOffset.FromUnixTimeSeconds(review.Time).DateTime,
                        isAddedGoogle = true,
                        status = true
                    };

                    await _context.Comments.AddAsync(newComment);
                }
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task<List<ResultCommentDto>> GetActiveCommentsAsync()
    { 
        var values = await _context.Comments
            .Where(x => x.status == true)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<ResultCommentDto>>(values);
    }
}