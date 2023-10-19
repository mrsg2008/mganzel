using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TourOfHeroes.Models;
using TourOfHeroes.Services;

namespace TourOfHeroes.Pages.Heroes
{
  public class HeroDetailModel : PageModel
  {
    private readonly IHeroService _heroService;

    private readonly IMessageService _messageService;

    [BindProperty]
    public Hero Hero { get; set; }

    public List<string> Messages { get; set;}

    public List<Hero> Heroes { get; set; }


    public HeroDetailModel(IHeroService heroService, IMessageService messageService)
    {
      _heroService = heroService;
      _messageService = messageService;
    }

    public IActionResult OnGet(int? id)
    {
      Messages = _messageService.GetMessages();

      if (id == null)
      {
        return NotFound();
      }

      Hero = _heroService.GetHero(id.Value);

      if (Hero == null)
      {
        return NotFound();
      }

      _messageService.AddMessage($"HeroDetail: Loaded hero with ID {id}");

      return Page();
    }

    public async Task<IActionResult> OnPostSaveAsync(Hero hero)
    {
      if (!ModelState.IsValid)
        return Page();

      await _heroService.UpdateHeroAsync(hero);

      return RedirectToPage("/Heroes/MyHeroes");
    }
  }
}
