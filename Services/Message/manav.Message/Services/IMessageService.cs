using manav.Message.Dtos;

namespace manav.Message.Services;

public interface IMessageService
{
    Task<List<ResultMessageDto>> GetAllMessageAsync();
    Task<List<ResultInboxMessageDto>> GetAllMessageInboxAsync(string id);
    Task<List<ResultSendboxMessageDto>> GetAllMessageSendboxAsync(string id);
    Task CreateMessageAsync(CreateMessageDto createMessageDto);
    Task UpdateMessageAsync(UpdateMessageDto updateMessageDto);
    Task DeleteMessageAsync(int id);
    Task <GetByIdMesageDto>  GetByIdMesageAsync(string id);
}