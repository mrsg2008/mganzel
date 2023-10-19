namespace TourOfHeroes.Services
{
  public interface IMessageService
  {
    List<string> Messages { get; }

    List<string> GetMessages();

    void AddMessage(string message);
    void ClearMessages();
  }
}
