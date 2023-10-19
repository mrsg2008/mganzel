using Microsoft.AspNetCore.Mvc.RazorPages;
using TourOfHeroes.Models;
using TourOfHeroes.Services;

namespace TourOfHeroes.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly IHeroService _heroService;
    private readonly IMessageService _messageService;
    public IObservable<List<Hero>> Heroes;

    public IndexModel(IHeroService heroService, IMessageService messageService, ILogger<IndexModel> logger)
    {
      _heroService = heroService;
      _messageService = messageService;
      _logger = logger;
    }

    public List<Hero> TopHeroes { get; set; }
    public List<string> Messages { get; set; }

    public void OnGet()
    {
      try
      {
        TopHeroes = _heroService.GetTopHeroes(5);
        Messages = _messageService.GetMessages();
      }
      catch (Exception ex)
      {
        _logger.LogError("An error occurred: {ErrorMessage}", ex.Message);
        throw;  // Re-throw the exception after logging
      }
    }

    //public IActionResult OnPostClearMessages()
    //{
    //  _messageService.ClearMessages();
    //  return RedirectToPage();
    //}
  }
}
