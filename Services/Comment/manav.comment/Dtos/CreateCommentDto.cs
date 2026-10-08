namespace manav.component.Dtos;

public class CreateCommentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserImageURL { get; set; }
    public string CommentDetail{ get; set; }
    public double Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool status { get; set; }
    public bool isAddedGoogle { get; set; }

}