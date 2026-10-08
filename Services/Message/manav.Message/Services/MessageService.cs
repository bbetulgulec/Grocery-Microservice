using AutoMapper;
using manav.Message.Data_Access_Layer.Context;
using manav.Message.Data_Access_Layer.Entities;
using manav.Message.Dtos;
using Microsoft.EntityFrameworkCore;

namespace manav.Message.Services;

public class MessageService:IMessageService
{
    private readonly MessageContext  _context;
    private readonly IMapper _mapper;

    public MessageService(MessageContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ResultMessageDto>> GetAllMessageAsync()
    {
        var values = await _context.UserMessages.ToListAsync();
        return _mapper.Map<List<ResultMessageDto>>(values);

    }

    public async Task<List<ResultInboxMessageDto>> GetAllMessageInboxAsync(string id)
    {
        var values = await _context.UserMessages.Where(x=>x.ReceiverId==id).ToListAsync();
        return _mapper.Map<List<ResultInboxMessageDto>>(values);
    }

    public async Task<List<ResultSendboxMessageDto>> GetAllMessageSendboxAsync(string id)
    {
        var values = await _context.UserMessages.Where(x=>x.SenderId==id).ToListAsync();
        return _mapper.Map<List<ResultSendboxMessageDto>>(values);
    }

    public async Task CreateMessageAsync(CreateMessageDto createMessageDto)
    {
        var value= _mapper.Map<UserMessage>(createMessageDto);
        await _context.UserMessages.AddAsync(value);
        await _context.SaveChangesAsync();
    }

    public  Task UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        var values = _mapper.Map<UserMessage>(updateMessageDto);
         _context.UserMessages.Update(values);
        return _context.SaveChangesAsync();
    }

    public async Task DeleteMessageAsync(int id)
    {
        var values = await _context.UserMessages.FindAsync(id);
       _context.UserMessages.Remove(values);
    }

    public async Task<GetByIdMesageDto> GetByIdMesageAsync(string id)
    {
        var value = await _context.UserMessages.FindAsync(id);
        return _mapper.Map<GetByIdMesageDto>(value);
    }
}