using manav.Message.Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace manav.Message.Data_Access_Layer.Context;

public class MessageContext:DbContext
{
    public MessageContext(DbContextOptions<MessageContext> options) : base(options)
    {
        
    }
    
    public DbSet<UserMessage> UserMessages { get; set; }
}