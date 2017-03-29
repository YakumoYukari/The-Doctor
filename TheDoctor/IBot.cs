namespace TheDoctor
{
    public interface IBot
    {
        bool IsMuted { get; set; }
        void Run();
    }
}