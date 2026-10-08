using manav.DtoLayer.MessageDtos.MessageDtos;

namespace manav.webUI.Services.MessageServices;

public class MessageService:IMessageService
{
    private readonly HttpClient _httpClient;

    public MessageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultInboxMessageDto>> GetAllMessageInboxAsync(string id)
    {
        var responseMassage =   await _httpClient.GetAsync("messages/usermessage/getmessageinbox?id="+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>();
        return values;
    }

    public async Task<List<ResultSendboxMessageDto>> GetAllMessageSendboxAsync(string id)
    {
        var responseMassage =   await _httpClient.GetAsync("messages/usermessage/getmessagesendbox?id="+id);
        var values = await responseMassage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>();
        return values;
    }
}