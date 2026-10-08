using manav.DtoLayer.MessageDtos.MessageDtos;

namespace manav.webUI.Services.MessageServices;

public interface IMessageService
{
    public  Task<List<ResultInboxMessageDto>> GetAllMessageInboxAsync(string id);

    public  Task<List<ResultSendboxMessageDto>> GetAllMessageSendboxAsync(string id);
}