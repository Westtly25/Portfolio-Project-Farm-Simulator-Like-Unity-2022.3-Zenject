namespace Assets.Scripts.Pause_System
{
    public interface IPauseHandler
    {
        bool IsPaused { get; }

        void CleanUp();
        void Register(IPauseListener listener);
        void SetPaused(bool isPaused);
        void UnRegister(IPauseListener listener);
    }
}