using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TourOfHeroes.Models;
using TourOfHeroes.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace TourOfHeroes.Pages.Heroes
{
  public class MyHeroesModel : PageModel
  {
    private readonly IHeroService _heroService;

    private readonly InMemoryDataService _inMemoryDataService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMemoryCache _cache;

    private readonly IMessageService _messageService;

    public List<string> Messages { get; set; }


    public List<Hero> Heroes { get; set; }

    public Hero SelectedHero {get; set; }

    [BindProperty]
    public string newHeroName { get; set; }



    public MyHeroesModel(IHeroService heroService, InMemoryDataService inMemoryDataService, IMemoryCache cache, IMessageService messageService, IHttpContextAccessor httpContextAccessor)
    {
      _heroService = heroService;
      _inMemoryDataService = inMemoryDataService;
      _cache = cache;
      _messageService = messageService;
      _httpContextAccessor = httpContextAccessor;
    }

    public void OnGet()
    {
      Messages = _messageService.GetMessages();

      // Try to get the list of heroes from the session
      var sessionHeroes = _httpContextAccessor.HttpContext.Session.GetString("Heroes");
      List<Hero> heroes;
      if (string.IsNullOrEmpty(sessionHeroes))
      {
        // If the list of heroes is not in the session, get it from the HeroService
        heroes = _heroService.GetHeroes();

        // Save the list of heroes in the session
        _httpContextAccessor.HttpContext.Session.SetString("Heroes", JsonConvert.SerializeObject(heroes));
      }
      else
      {
        heroes = JsonConvert.DeserializeObject<List<Hero>>(sessionHeroes);
      }

      Heroes = heroes;
    }

    public void OnPostDelete(int id)
    {
      _heroService.DeleteHero(id);
      Heroes = _heroService.GetHeroes();

      // Save the updated list of heroes in the session
      _httpContextAccessor.HttpContext.Session.SetString("Heroes", JsonConvert.SerializeObject(Heroes));
    }

    public IActionResult OnPostAdd()
    {

      var newHero = new Hero { Name = newHeroName };
      _heroService.AddHero(newHero);
      Heroes = _heroService.GetHeroes();

      // Save the updated list of heroes in the session
      _httpContextAccessor.HttpContext.Session.SetString("Heroes", JsonConvert.SerializeObject(Heroes));

      newHeroName = string.Empty;

      return RedirectToPage();
    }

    public void OnPostSelectHero(Hero hero)
    {
      SelectedHero = hero;
      _messageService.AddMessage($"MyHeroes: Selected hero id={hero.Id}");
    }

  }
}
