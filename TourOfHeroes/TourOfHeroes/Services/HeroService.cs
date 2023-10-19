using TourOfHeroes.Models;

namespace TourOfHeroes.Services
{
  public class HeroService : IHeroService
  {
    private readonly ILogger<HeroService> _logger;
    private readonly InMemoryDataService _inMemoryDataService;
    private readonly IMessageService _messageService;
    private List<Hero> heroes;

    public HeroService(InMemoryDataService inMemoryDataService, ILogger<HeroService> logger, IMessageService messageService)
    {
      _inMemoryDataService = inMemoryDataService;
      heroes = _inMemoryDataService.GetHeroes();
      _logger = logger;
      _messageService = messageService;
    }

    public List<Hero> GetHeroes()
    {
      _messageService.AddMessage("HeroService: fetched heroes");
      return heroes;
    }

    public Hero GetHero(int id)
    {
      return heroes.FirstOrDefault(hero => hero.Id == id);
    }

    public List<Hero> GetMatchingHeroes(string searchTerm)
    {
      try
      {
        searchTerm = searchTerm?.ToLower() ?? "";

        return _inMemoryDataService.GetHeroes()
            .Where(hero => hero.Name.ToLower().Contains(searchTerm))
            .ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError("An error occurred: {ErrorMessage}", ex.Message);
        throw;
      }
    }

    public void AddHero(Hero hero)
    {
      if (hero == null)
        throw new ArgumentNullException(nameof(hero));

      if (heroes.Any(h => h.Id == hero.Id))
        throw new ArgumentException("Hero already exists");

      // Assign a unique ID
      hero.Id = heroes.Max(h => h.Id) + 1;

      heroes.Add(hero);
      _inMemoryDataService.SetHeroes(heroes);
    }

    public void DeleteHero(int id)
    {
      var hero = heroes.FirstOrDefault(h => h.Id == id);
      if (hero != null)
      {
        heroes.Remove(hero);
        _inMemoryDataService.SetHeroes(heroes);
      }
    }

    public async Task UpdateHeroAsync(Hero updatedHero)
    {
      await _inMemoryDataService.UpdateHeroAsync(updatedHero);

      var existingHero = heroes.FirstOrDefault(hero => hero.Id == updatedHero.Id);

      if (existingHero != null)
      {
        existingHero.Name = updatedHero.Name;
        return;
      }

      throw new ArgumentException("Hero not found for update.");
    }


    public List<Hero> GetTopHeroes(int count)
    {
      // Sort the heroes based on a criteria (e.g., assuming sorting by ID for simplicity)
      List<Hero> sortedHeroes = heroes.OrderBy(hero => hero.Id).ToList();

      // Take the top 'count' heroes
      List<Hero> topHeroes = sortedHeroes.Take(count).ToList();

      return topHeroes;
    }
  }
}
