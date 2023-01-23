
namespace Domain.Notifications
{
    public class Notificaion : INotification
    {
        public Notificaion()
        {
            Messages = new List<string>();
        }


        public List<string> Messages { get; }

        public bool Valid => Messages.Count == 0;

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}
