namespace manav.DtoLayer.CommentDtos.CommentDtos;

public class UpdateCommentDto
{
    public string UserCommentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserImageURL { get; set; }
    public string CommentDetail{ get; set; }
    public double Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool status { get; set; }
    public bool isAddedGoogle { get; set; }

    
}