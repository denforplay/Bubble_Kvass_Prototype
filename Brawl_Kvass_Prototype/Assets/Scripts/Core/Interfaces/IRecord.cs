namespace Core.Interfaces
{
    public interface IRecord
    {
        bool IsTarget((object, object) pair);
    }
}