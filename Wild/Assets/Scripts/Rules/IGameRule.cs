namespace Rules
{
    public interface IGameRule
    {
        string RuleName { get; }
        void Activate();
        void Deactivate();
    }
}