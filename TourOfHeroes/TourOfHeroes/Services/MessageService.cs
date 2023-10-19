using Newtonsoft.Json;

namespace TourOfHeroes.Services
{
  public class MessageService : IMessageService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public List<string> Messages { get; set; }

    public MessageService(IHttpContextAccessor httpContextAccessor)
    {
      Messages = new List<string>();
      _httpContextAccessor = httpContextAccessor;
    }

    public List<string> GetMessages()
    {
      var messagesJson = _httpContextAccessor.HttpContext.Session.GetString("Messages");
      return messagesJson == null ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(messagesJson);
    }
    public void AddMessage(string message)
    {
      var messages = GetMessages();
      messages.Add(message);
      _httpContextAccessor.HttpContext.Session.SetString("Messages", JsonConvert.SerializeObject(messages));
    }

    public void ClearMessages()
    {
      _httpContextAccessor.HttpContext.Session.Remove("Messages");
    }
  }
}
