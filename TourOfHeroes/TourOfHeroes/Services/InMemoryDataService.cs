using TourOfHeroes.Models;

namespace TourOfHeroes.Services
{
  public class InMemoryDataService
  {
    private static List<Hero> _heroes;
    private static int _nextId;

    static InMemoryDataService()
    {
      InitializeData();
    }

    private static void InitializeData()
    {
      _heroes = new List<Hero>
      {
            new Hero { Id = 12, Name = "Dr. Nice" },
            new Hero { Id = 13, Name = "Bombasto" },
            new Hero { Id = 14, Name = "Celeritas" },
            new Hero { Id = 15, Name = "Magneta" },
            new Hero { Id = 16, Name = "RubberMan" },
            new Hero { Id = 17, Name = "Dynama" },
            new Hero { Id = 18, Name = "Dr. IQ" },
            new Hero { Id = 19, Name = "Magma" },
            new Hero { Id = 20, Name = "Tornado" }
      };

      _nextId = _heroes.Count + 1;
    }

    public List<Hero> GetHeroes()
    {
      return _heroes;
    }

    public void SetHeroes(List<Hero> heroes)
    {
      _heroes = heroes;
      _nextId = _heroes.Count + 1;
    }

    public async Task UpdateHeroAsync(Hero updatedHero)
    {
      // Find the hero to update by its ID
      var existingHero = _heroes.FirstOrDefault(hero => hero.Id == updatedHero.Id);

      if (existingHero != null)
      {
        // Update the hero's name
        existingHero.Name = updatedHero.Name;
        return;
      }

      // Handle if the hero to update was not found
      throw new ArgumentException("Hero not found for update.");
    }
    public Hero GetHero(int id)
    {
      return _heroes.FirstOrDefault(hero => hero.Id == id);
    }

    public int GenId()
    {
      return _nextId++;
    }

    public void DeleteHero(int id)
    {
      var heroToRemove = _heroes.FirstOrDefault(hero => hero.Id == id);

      if (heroToRemove != null)
      {
        _heroes.Remove(heroToRemove);
      }
      else
      {
        throw new ArgumentException("Hero not found for deletion.");
      }
    }
  }
}
