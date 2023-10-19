using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TourOfHeroes.Models;
using TourOfHeroes.Services;

namespace TourOfHeroes.Pages
{
  public class HeroSearchModel : PageModel
  {
    private readonly IHeroService _heroService;

    private readonly IMessageService _messageService;

    [BindProperty(SupportsGet = true)] public string SearchTerm { get; set; }

    public List<Hero> Suggestions { get; set; }

    public List<string> Messages { get; set; }

    public HeroSearchModel(IHeroService heroService, IMessageService messageService)
    {
      _heroService = heroService;
      _messageService = messageService;
      Suggestions = new List<Hero>();
    }
    public IActionResult OnGet(string searchTerm)
    {
      Messages = _messageService.GetMessages();

      if (!string.IsNullOrEmpty(SearchTerm))
      {
        // Perform your search logic based on SearchTerm
        Suggestions = _heroService.GetMatchingHeroes(searchTerm);
      }

      return Page();
    }

  }
}
