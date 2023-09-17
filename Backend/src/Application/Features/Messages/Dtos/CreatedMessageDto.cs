namespace Application.Features.Messages;

public class CreatedMessageDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
    public string SenderName { get; set; }
    public string SenderImagePath { get; set; }
    public DateTime CreatedDate { get; set; }
}