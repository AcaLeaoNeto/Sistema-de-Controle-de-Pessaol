
namespace Domain.Notifications
{
    public interface INotification
    {
       List<String> Messages { get; }
       bool Valid { get; }
       void AddMessage(string message);

    }
}
