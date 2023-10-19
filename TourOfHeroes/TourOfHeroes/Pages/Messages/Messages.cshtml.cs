using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TourOfHeroes.Models;
using TourOfHeroes.Services;

namespace TourOfHeroes.Pages.Messages
{
  public class MessagesModel : PageModel
  {
    private readonly IMessageService _messageService;

    private readonly IHeroService _heroService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public List<string> Messages { get; private set;}

    public List<Hero>? Heroes { get; private set; }

    public MessagesModel(IMessageService messageService, IHeroService heroService, IHttpContextAccessor httpContextAccessor)
    {
      _messageService = messageService;
      _heroService = heroService;
      _httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
      if (_httpContextAccessor.HttpContext.Session.GetString("Heroes") != null)
      {
        var heroes = JsonConvert.DeserializeObject<List<Hero>>(_httpContextAccessor.HttpContext.Session.GetString("Heroes"));
        ViewData["Heroes"] = heroes;
        Heroes = heroes;
      }

      var messages = JsonConvert.DeserializeObject<List<string>>(_httpContextAccessor.HttpContext.Session.GetString("Messages"));
      ViewData["Messages"] = messages;
      Messages = messages;
    }

    public IActionResult OnPost()
    {
      _messageService.ClearMessages();
      //_httpContextAccessor.HttpContext.Session.SetString("Messages", JsonConvert.SerializeObject(_messageService.GetMessages()));
      return RedirectToPage("/Index");
    }

    // Property to access the message service in the Razor page
    public IMessageService MessageService => _messageService;

  }
}
