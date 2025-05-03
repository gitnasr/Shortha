namespace Shortha.Interfaces
{
    public interface IAppUser
    {
        bool isBlocked { get; set; }
        bool isPremium { get; set; }
    }
}