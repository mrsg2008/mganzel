using TourOfHeroes.Models;

namespace TourOfHeroes.Services
{
  public interface IHeroService
  {
    List<Hero> GetHeroes();
    Hero GetHero(int id);
    List<Hero> GetMatchingHeroes(string searchTerm);
    void AddHero(Hero hero);
    void DeleteHero(int id);
    Task UpdateHeroAsync(Hero updatedHero);
    List<Hero> GetTopHeroes(int count);

  }
}
