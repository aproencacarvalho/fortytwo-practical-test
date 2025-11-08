namespace Fortytwo.PracticalTest.Domain.Models;

public class PostEntityAttributes
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public int CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}