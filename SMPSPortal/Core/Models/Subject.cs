namespace SmpsPortal.Core.Models
{
    public interface Subject
    {
        void registerObserver(UserNotification o);
        void removeObserver(UserNotification o);
        void notifyObservers();
    }
}
